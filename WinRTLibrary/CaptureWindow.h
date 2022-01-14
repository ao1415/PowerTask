#pragma once

#include "pch.h"

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

class CaptureWindow {
public:
	using FrameArrivedCallback = std::function<void(winrt::com_ptr<ID3D11Texture2D> texture, SizeInt32 size)>;

private:

	GraphicsCaptureItem m_item{ nullptr };
	Direct3D11CaptureFramePool m_framePool{ nullptr };
	GraphicsCaptureSession m_session{ nullptr };
	SizeInt32 m_lastSize{};

	winrt::com_ptr<ID3D11Device> m_d3dDevice{ nullptr };
	IDirect3DDevice m_device{ nullptr };
	winrt::com_ptr<IDXGISwapChain1> m_swapChain{ nullptr };
	winrt::com_ptr<ID3D11DeviceContext> m_d3dContext{ nullptr };

	std::atomic<bool> m_closed = false;
	Direct3D11CaptureFramePool::FrameArrived_revoker m_frameArrived;

	FrameArrivedCallback m_callback;

private:
	void OnFrameArrived(const Direct3D11CaptureFramePool& sender, const winrt::Windows::Foundation::IInspectable& args) {

		bool newSize = false;
		{
			auto frame = sender.TryGetNextFrame();
			SizeInt32 frameContentSize = frame.ContentSize();

			if (frameContentSize.Width != m_lastSize.Width ||
				frameContentSize.Height != m_lastSize.Height)
			{
				newSize = true;
				m_lastSize = frameContentSize;
				m_swapChain->ResizeBuffers(
					2,
					static_cast<uint32_t>(m_lastSize.Width),
					static_cast<uint32_t>(m_lastSize.Height),
					static_cast<DXGI_FORMAT>(DirectXPixelFormat::B8G8R8A8UIntNormalized),
					0);
			}

			{
				auto frameSurface = GetDXGIInterfaceFromObject<ID3D11Texture2D>(frame.Surface());

				winrt::com_ptr<ID3D11Texture2D> backBuffer;
				winrt::check_hresult(m_swapChain->GetBuffer(0, guid_of<ID3D11Texture2D>(), backBuffer.put_void()));
				m_d3dContext->CopyResource(backBuffer.get(), frameSurface.get());
				m_callback(backBuffer, frameContentSize);
			}
		}

		DXGI_PRESENT_PARAMETERS presentParameters = { 0 };
		m_swapChain->Present1(1, 0, &presentParameters);

		if (newSize)
		{
			m_framePool.Recreate(m_device, DirectXPixelFormat::B8G8R8A8UIntNormalized, 2, m_lastSize);
		}
	}

	void CheckClosed() {
		if (m_closed.load() == true) {
			throw winrt::hresult_error(RO_E_CLOSED);
		}
	}

public:

	CaptureWindow(winrt::com_ptr<ID3D11Device>& d3dDevice, const IDirect3DDevice& device, const GraphicsCaptureItem& item)
		: m_d3dDevice(d3dDevice), m_item(item), m_device(device) {

		m_d3dDevice->GetImmediateContext(m_d3dContext.put());

		SizeInt32 size = m_item.Size();

		m_swapChain = CreateDXGISwapChain(
			m_d3dDevice,
			static_cast<uint32_t>(size.Width),
			static_cast<uint32_t>(size.Height),
			static_cast<DXGI_FORMAT>(DirectXPixelFormat::B8G8R8A8UIntNormalized),
			2);

		m_framePool = Direct3D11CaptureFramePool::Create(m_device, DirectXPixelFormat::B8G8R8A8UIntNormalized, 2, size);
		m_session = m_framePool.CreateCaptureSession(m_item);
		m_session.IsCursorCaptureEnabled(false);
		m_lastSize = size;

		m_frameArrived = m_framePool.FrameArrived(winrt::auto_revoke, { this, &CaptureWindow::OnFrameArrived });

	}
	~CaptureWindow() {
		Close();
	}

	void StartCapture(FrameArrivedCallback callback) {
		CheckClosed();
		m_callback = callback;
		m_session.StartCapture();
	}

	void Close() {
		bool expected = false;
		if (m_closed.compare_exchange_strong(expected, true))
		{
			m_frameArrived.revoke();
			m_framePool.Close();
			m_session.Close();

			m_swapChain = nullptr;
			m_framePool = nullptr;
			m_session = nullptr;
			m_item = nullptr;
		}
	}

};
