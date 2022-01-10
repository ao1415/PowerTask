#pragma once

#include "Class.g.h"

#include <inspectable.h>
#include <windows.h>

// WinRT
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.System.h>
#include <winrt/Windows.UI.h>
#include <winrt/Windows.UI.Composition.h>
#include <winrt/Windows.UI.Composition.Desktop.h>
#include <winrt/Windows.UI.Popups.h>
#include <winrt/Windows.Graphics.Capture.h>
#include <winrt/Windows.Graphics.DirectX.h>
#include <winrt/Windows.Graphics.DirectX.Direct3d11.h>

#include <windows.graphics.capture.interop.h>
#include <windows.graphics.capture.h>
#include <windows.ui.composition.interop.h>
#include <DispatcherQueue.h>

// STL
#include <atomic>
#include <string>

// D3D
#include <d3d11_4.h>
#include <dxgi1_6.h>
#include <d2d1_3.h>
#include <wincodec.h>

// Helpers
#include "composition.interop.h"
#include "d3dHelpers.h"
#include "direct3d11.interop.h"
#include "capture.interop.h"

#define STB_IMAGE_WRITE_IMPLEMENTATION
#define __STDC_LIB_EXT1__
#include "stb_image_write.h"

#pragma comment(lib, "windowsapp.lib")

using namespace winrt;
using namespace Windows;
using namespace Windows::Foundation;
using namespace Windows::System;
using namespace Windows::Graphics;
using namespace Windows::Graphics::Capture;
using namespace Windows::Graphics::DirectX;
using namespace Windows::Graphics::DirectX::Direct3D11;
using namespace Windows::Foundation::Numerics;
using namespace Windows::UI;
using namespace Windows::UI::Composition;

namespace winrt::WinRTLibrary::implementation {
	class Class : public ClassT<Class> {
	private:

		winrt::com_ptr<ID3D11DeviceContext> d3dContext{ nullptr };
		winrt::com_ptr<ID3D11Texture2D> bufferTexture{ nullptr };
		std::atomic<bool> state{ false };

		winrt::hstring filePath{};

	private:

		bool ReadTexture(ID3D11Texture2D* tex, int width, int height) {
			com_ptr<ID3D11Device> device;
			com_ptr<ID3D11DeviceContext> ctx;
			tex->GetDevice(device.put());
			device->GetImmediateContext(ctx.put());

			// create query
			com_ptr<ID3D11Query> query_event;
			{
				D3D11_QUERY_DESC qdesc = { D3D11_QUERY_EVENT , 0 };
				device->CreateQuery(&qdesc, query_event.put());
			}

			// create staging texture
			com_ptr<ID3D11Texture2D> staging;
			{
				D3D11_TEXTURE2D_DESC tmp;
				tex->GetDesc(&tmp);
				D3D11_TEXTURE2D_DESC desc{ (UINT)width, (UINT)height, 1, 1, tmp.Format, { 1, 0 }, D3D11_USAGE_STAGING, 0, D3D11_CPU_ACCESS_READ, 0 };
				device->CreateTexture2D(&desc, nullptr, staging.put());
			}

			// dispatch copy
			{
				D3D11_BOX box{};
				box.right = width;
				box.bottom = height;
				box.back = 1;
				ctx->CopySubresourceRegion(staging.get(), 0, 0, 0, 0, tex, 0, &box);
				ctx->End(query_event.get());
				ctx->Flush();
			}

			// wait for copy to complete
			int wait_count = 0;
			while (ctx->GetData(query_event.get(), nullptr, 0, 0) == S_FALSE) {
				++wait_count; // just for debug
			}

			// map
			D3D11_MAPPED_SUBRESOURCE mapped{};
			if (SUCCEEDED(ctx->Map(staging.get(), 0, D3D11_MAP_READ, 0, &mapped))) {
				D3D11_TEXTURE2D_DESC desc{};
				staging->GetDesc(&desc);

				SaveAsPNG(filePath, width, height, mapped.RowPitch, mapped.pData);
				ctx->Unmap(staging.get(), 0);
				return true;
			}
			return false;
		}

		bool SaveAsPNG(const winrt::hstring path, int w, int h, int src_stride, const void* data)
		{
			std::vector<byte> buf(w * h * 4);
			int dst_stride = w * 4;
			auto src = (const byte*)data;
			auto dst = (byte*)buf.data();

			for (int i = 0; i < h; ++i) {
				auto s = src + (src_stride * i);
				auto d = dst + (dst_stride * i);
				for (int j = 0; j < w; ++j) {
					d[0] = s[2];
					d[1] = s[1];
					d[2] = s[0];
					d[3] = s[3];
					s += 4;
					d += 4;
				}
			}
			return stbi_write_png(winrt::to_string(path).c_str(), w, h, 4, buf.data(), dst_stride);
		}

		void OnFrameArrived(const Direct3D11CaptureFramePool& sender, const IInspectable&) {
			{
				auto frame = sender.TryGetNextFrame();
				auto size = frame.ContentSize();

				com_ptr<ID3D11Texture2D> surface;
				frame.Surface().as<IDirect3DDxgiInterfaceAccess>()->GetInterface(guid_of<ID3D11Texture2D>(), surface.put_void());
				ReadTexture(surface.get(), size.Width, size.Height);
			}

			// 終了通知
			std::atomic_store(&state, false);
			std::atomic_notify_one(&state);
		}

		void capture(HWND hwnd) {

			auto d3dDevice = CreateD3DDevice();
			auto dxgiDevice = d3dDevice.as<IDXGIDevice>();
			auto device = CreateDirect3DDevice(dxgiDevice.get());

			auto item = CreateCaptureItemForWindow(hwnd);

			capture(device, item);
		}

		void capture(const IDirect3DDevice& device, const GraphicsCaptureItem& item) {

			const auto PixcelFormat = DirectXPixelFormat::B8G8R8A8UIntNormalized;

			auto d3dDevice = GetDXGIInterfaceFromObject<ID3D11Device>(device);
			d3dDevice->GetImmediateContext(d3dContext.put());

			auto size = item.Size();

			auto framePool = Direct3D11CaptureFramePool::Create(device, PixcelFormat, 2, size);
			auto session = framePool.CreateCaptureSession(item);
			session.IsCursorCaptureEnabled(false);
			auto frameArrived = framePool.FrameArrived(auto_revoke, { this, &Class::OnFrameArrived });

			state.store(false);
			// キャプチャ開始
			session.StartCapture();

			// 処理終了まで待機
			std::atomic_wait(&state, true);

			// 終了処理
			frameArrived.revoke();
			framePool.Close();
			session.Close();

		}

	public:

		Class() = default;

		void start(winrt::hstring path) {
			
			filePath = path;

			capture(GetActiveWindow());
		}

	};
}

namespace winrt::WinRTLibrary::factory_implementation {
	struct Class : ClassT<Class, implementation::Class> {
	};
}
