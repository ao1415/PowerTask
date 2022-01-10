#pragma once

#include <d3d11.h>

#include <winrt/Windows.Graphics.h>
#include <winrt/Windows.Graphics.Capture.h>
#include <winrt/Windows.Graphics.DirectX.Direct3D11.h>

#include "Class.g.h"

namespace winrt::WinRTLibrary::implementation {
	class GraphicsCapture : public ClassT<GraphicsCapture> {
	private:

		int32_t m_value = 0;

		std::optional<winrt::com_ptr<ID3D11Texture2D>> getCapture() {

			// アクティブなウィンドウをキャプチャ
			HWND hwnd = GetForegroundWindow();

			winrt::Windows::Graphics::DirectX::Direct3D11::IDirect3DDevice m_device_rt{ nullptr };
			winrt::Windows::Graphics::Capture::Direct3D11CaptureFramePool m_frame_pool{ nullptr };
			winrt::Windows::Graphics::Capture::GraphicsCaptureItem m_capture_item{ nullptr };
			winrt::Windows::Graphics::Capture::GraphicsCaptureSession m_capture_session{ nullptr };
			winrt::Windows::Graphics::Capture::Direct3D11CaptureFramePool::FrameArrived_revoker m_frame_arrived;

			// デバイス作成
			winrt::com_ptr<ID3D11Device> device;
			D3D11CreateDevice(nullptr, D3D_DRIVER_TYPE_HARDWARE, nullptr, D3D11_CREATE_DEVICE_BGRA_SUPPORT, nullptr, 0, D3D11_SDK_VERSION, device.put(), nullptr, nullptr);

			// WinRT 版デバイス作成
			auto dxgi = device.as<IDXGIDevice>();
			winrt::com_ptr<IInspectable> device_rt;
			CreateDirect3D11DeviceFromDXGIDevice(dxgi.get(), device_rt.put());
			m_device_rt = device_rt.as<IDirect3DDevice>();

			// CaptureItem 作成
			auto factory = get_activation_factory<GraphicsCaptureItem>();
			auto interop = factory.as<IGraphicsCaptureItemInterop>();
			interop->CreateForWindow(hwnd, guid_of<winrt::Windows::Graphics::Capture::IGraphicsCaptureItem>(), put_abi(m_capture_item));
			if (m_capture_item) {
				// FramePool 作成
				auto size = m_capture_item.Size();
				m_frame_pool = Direct3D11CaptureFramePool::CreateFreeThreaded(m_device_rt, DirectXPixelFormat::B8G8R8A8UIntNormalized, 1, size);
				m_frame_arrived = m_frame_pool.FrameArrived(auto_revoke, [](Direct3D11CaptureFramePool const& sender, IInspectable const& args) {
					auto frame = sender.TryGetNextFrame();

					// キャプチャ結果
					winrt::com_ptr<ID3D11Texture2D> surface;
					frame.Surface().as<IDirect3DDxgiInterfaceAccess>()->GetInterface(guid_of<ID3D11Texture2D>(), surface.put_void());
					});

				// キャプチャ開始
				m_capture_session = m_frame_pool.CreateCaptureSession(m_capture_item);
				m_capture_session.StartCapture();

				return std::nullopt;
			}
			else {
				return std::nullopt;
			}
		}

	public:
		GraphicsCapture() = default;

		bool saveActiveWindow() {

		}

	};
}

namespace winrt::WinRTLibrary::factory_implementation {
	struct Class : ClassT<Class, implementation::GraphicsCapture> {
	};
}
