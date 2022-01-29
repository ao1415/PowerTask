#pragma once

#include "GraphicsCapture.g.h"

#include <condition_variable>
#include <functional>

#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.System.h>
#include <winrt/Windows.Graphics.Capture.h>
#include <winrt/Windows.Graphics.DirectX.Direct3d11.h>
#include <windows.graphics.directx.direct3d11.interop.h>
#include <Windows.Graphics.Capture.Interop.h>
#include <d3d11.h>

namespace winrt::WinRTComponent::implementation
{
	class GraphicsCapture : public GraphicsCaptureT<GraphicsCapture> {
	private:
		using Callback = std::function<void(ID3D11Texture2D*, int w, int h)>;

		com_ptr<ID3D11Device> m_device;
		com_ptr<ID3D11DeviceContext> m_context;

		winrt::Windows::Graphics::DirectX::Direct3D11::IDirect3DDevice m_device_rt{ nullptr };
		winrt::Windows::Graphics::Capture::Direct3D11CaptureFramePool m_frame_pool{ nullptr };
		winrt::Windows::Graphics::Capture::GraphicsCaptureItem m_capture_item{ nullptr };
		winrt::Windows::Graphics::Capture::GraphicsCaptureSession m_capture_session{ nullptr };
		winrt::Windows::Graphics::Capture::Direct3D11CaptureFramePool::FrameArrived_revoker m_frame_arrived;

		Callback m_callback;

		HRESULT createDevice();

		bool startImpl(intptr_t hwnd);

		winrt::Windows::Graphics::Capture::GraphicsCaptureItem createGraphicsCaptureItem(intptr_t hwnd);

		void onFrameArrived(
			const winrt::Windows::Graphics::Capture::Direct3D11CaptureFramePool& sender,
			const winrt::Windows::Foundation::IInspectable& args);

		bool start(intptr_t hwnd);
		void stop();

		std::vector<byte> readTexture(ID3D11Texture2D* tex, int width, int height);
		std::vector<byte> getBitArray(const void* data, int width, int height, int src_stride);

	public:
		GraphicsCapture();
		~GraphicsCapture();

		winrt::com_array<byte> GetActiveWindow(intptr_t hwnd);

	};
}

namespace winrt::WinRTComponent::factory_implementation
{
	class GraphicsCapture : public GraphicsCaptureT<GraphicsCapture, implementation::GraphicsCapture> {
	};
}
