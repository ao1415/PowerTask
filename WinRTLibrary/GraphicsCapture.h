#pragma once

#include <memory>

#include "GraphicsCapture.g.h"

#include "CaptureWindow.h"

namespace winrt::WinRTLibrary::implementation {
	class GraphicsCapture : public GraphicsCaptureT<GraphicsCapture> {
	private:

		winrt::com_ptr<ID3D11Device> m_d3dDevice{ nullptr };
		IDirect3DDevice m_device{ nullptr };

		std::unique_ptr<CaptureWindow> m_capture{ nullptr };

	private:

		void CreateDevice() {
			m_d3dDevice = CreateD3DDevice();
			auto dxgiDevice = m_d3dDevice.as<IDXGIDevice>();
			m_device = CreateDirect3DDevice(dxgiDevice.get());
		}

		void StartCapture(HWND hwnd, CaptureWindow::FrameArrivedCallback callback) {
			auto item = CreateCaptureItemForWindow(hwnd);
			m_capture = std::make_unique<CaptureWindow>(m_d3dDevice, m_device, item);
			m_capture->StartCapture(callback);
		}

		void StopCapture() {
			if (m_capture) {
				m_capture->Close();
			}
		}

		std::vector<byte> GetBitmap(winrt::com_ptr<ID3D11Texture2D> texture, SizeInt32 size) {

			com_ptr<ID3D11Device> device;
			com_ptr<ID3D11DeviceContext> ctx;
			texture->GetDevice(device.put());
			device->GetImmediateContext(ctx.put());

			com_ptr<ID3D11Query> query_event;
			{
				D3D11_QUERY_DESC qdesc = { D3D11_QUERY_EVENT , 0 };
				device->CreateQuery(&qdesc, query_event.put());
			}

			com_ptr<ID3D11Texture2D> staging;
			{
				D3D11_TEXTURE2D_DESC tmp;
				texture->GetDesc(&tmp);
				D3D11_TEXTURE2D_DESC desc{ (UINT)size.Width, (UINT)size.Height, 1, 1, tmp.Format, { 1, 0 }, D3D11_USAGE_STAGING, 0, D3D11_CPU_ACCESS_READ, 0 };
				device->CreateTexture2D(&desc, nullptr, staging.put());
			}

			{
				D3D11_BOX box{};
				box.right = size.Width;
				box.bottom = size.Height;
				box.back = 1;
				ctx->CopySubresourceRegion(staging.get(), 0, 0, 0, 0, texture.get(), 0, &box);
				ctx->End(query_event.get());
				ctx->Flush();
			}

			int wait_count = 0;
			while (ctx->GetData(query_event.get(), nullptr, 0, 0) == S_FALSE) {
				++wait_count;
			}

			D3D11_MAPPED_SUBRESOURCE mapped{};
			std::vector<byte> bitmap;
			if (SUCCEEDED(ctx->Map(staging.get(), 0, D3D11_MAP_READ, 0, &mapped))) {
				D3D11_TEXTURE2D_DESC desc{};
				staging->GetDesc(&desc);

				bitmap = GetBitmap(size, mapped.pData, mapped.RowPitch);
				ctx->Unmap(staging.get(), 0);
			}

			return bitmap;
		}

		std::vector<byte> GetBitmap(SizeInt32 size, void* pData, UINT pitch) {

			std::vector<byte> buf(size.Width * size.Height * 4);
			auto src = reinterpret_cast<const byte*>(pData);

			for (int y = 0; y < size.Height; y++) {
				for (int x = 0; x < size.Width; x++) {
					const size_t index = (x + size.Width * static_cast<size_t>(y)) * 4;
					buf[index + 0] = src[index + 2];
					buf[index + 1] = src[index + 1];
					buf[index + 2] = src[index + 0];
					buf[index + 3] = src[index + 3];
				}
			}

			return buf;
		}

	public:

		GraphicsCapture() {
			CreateDevice();
		}

		winrt::com_array<byte> GetWindowCapture(intptr_t hwnd) {
			HWND activehWnd = reinterpret_cast<HWND>(hwnd);

			std::atomic<bool> state{ true };
			std::vector<byte> bitmap{};

			StartCapture(activehWnd, [&](winrt::com_ptr<ID3D11Texture2D> texture, SizeInt32 size) {

				bitmap = GetBitmap(texture, size);

				state.store(false);
				state.notify_one();
				});

			state.wait(true);
			StopCapture();

			return winrt::com_array(bitmap);
		}

	};
}

namespace winrt::WinRTLibrary::factory_implementation {
	struct GraphicsCapture : GraphicsCaptureT<GraphicsCapture, implementation::GraphicsCapture> {
	};
}
