#include "pch.h"
#include "GraphicsCapture.h"
#include "GraphicsCapture.g.cpp"

namespace winrt::WinRTComponent::implementation {

	HRESULT GraphicsCapture::createDevice() {

		UINT flags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;
#ifdef _DEBUG
		flags |= D3D11_CREATE_DEVICE_DEBUG;
#endif

		check_hresult(::D3D11CreateDevice(
			nullptr,
			D3D_DRIVER_TYPE_HARDWARE,
			nullptr,
			flags,
			nullptr,
			0,
			D3D11_SDK_VERSION,
			m_device.put(),
			nullptr,
			nullptr
		));
		m_device->GetImmediateContext(m_context.put());

		auto dxgi = m_device.as<IDXGIDevice>();
		com_ptr<::IInspectable> device_rt;
		check_hresult(::CreateDirect3D11DeviceFromDXGIDevice(dxgi.get(), device_rt.put()));
		m_device_rt = device_rt.as<winrt::Windows::Graphics::DirectX::Direct3D11::IDirect3DDevice>();

		return S_OK;
	}

	bool GraphicsCapture::startImpl(intptr_t hwnd) {
		stop();

		// create capture item
		m_capture_item = createGraphicsCaptureItem(hwnd);

		if (m_capture_item) {
			// create frame pool
			auto size = m_capture_item.Size();
			m_frame_pool = winrt::Windows::Graphics::Capture::Direct3D11CaptureFramePool::CreateFreeThreaded(m_device_rt, winrt::Windows::Graphics::DirectX::DirectXPixelFormat::B8G8R8A8UIntNormalized, 1, size);
			m_frame_arrived = m_frame_pool.FrameArrived(auto_revoke, { this, &GraphicsCapture::onFrameArrived });

			// capture start
			m_capture_session = m_frame_pool.CreateCaptureSession(m_capture_item);
			m_capture_session.IsCursorCaptureEnabled(false);
			//m_capture_session.IsBorderRequired(false);
			m_capture_session.StartCapture();
			return true;
		}
		else {
			return false;
		}
	}

	winrt::Windows::Graphics::Capture::GraphicsCaptureItem GraphicsCapture::createGraphicsCaptureItem(intptr_t hwnd) {
		winrt::Windows::Graphics::Capture::GraphicsCaptureItem item = nullptr;
		const auto factory = get_activation_factory<Windows::Graphics::Capture::GraphicsCaptureItem>();
		const auto interop = factory.as<IGraphicsCaptureItemInterop>();
		interop->CreateForWindow(reinterpret_cast<HWND>(hwnd), guid_of<ABI::Windows::Graphics::Capture::IGraphicsCaptureItem>(), reinterpret_cast<void**>(put_abi(item)));

		return item;
	}

	void GraphicsCapture::onFrameArrived(const winrt::Windows::Graphics::Capture::Direct3D11CaptureFramePool& sender, const winrt::Windows::Foundation::IInspectable& args) {
		auto frame = sender.TryGetNextFrame();
		auto size = frame.ContentSize();

		com_ptr<ID3D11Texture2D> surface;
		frame.Surface().as<::Windows::Graphics::DirectX::Direct3D11::IDirect3DDxgiInterfaceAccess>()->GetInterface(guid_of<ID3D11Texture2D>(), surface.put_void());
		m_callback(surface.get(), size.Width, size.Height);
	}

	GraphicsCapture::GraphicsCapture() {
		check_hresult(createDevice());
	}

	GraphicsCapture::~GraphicsCapture() {
		stop();
	}

	bool GraphicsCapture::start(intptr_t hwnd) {
		return startImpl(hwnd);
	}

	void GraphicsCapture::stop() {
		m_frame_arrived.revoke();
		m_capture_session = nullptr;
		if (m_frame_pool) {
			m_frame_pool.Close();
			m_frame_pool = nullptr;
		}
		m_capture_item = nullptr;
	}

	std::vector<byte> GraphicsCapture::readTexture(ID3D11Texture2D* tex, int width, int height) {
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

			std::vector<byte> buf = getBitArray(mapped.pData, width, height, mapped.RowPitch);
			ctx->Unmap(staging.get(), 0);
			return buf;
		}
		return std::vector<byte>();
	}

	std::vector<byte> GraphicsCapture::getBitArray(const void* data, int width, int height, int src_stride) {

		std::vector<byte> buf(width * height * 4);
		int dst_stride = width * 4;
		auto src = (const byte*)data;
		auto dst = (byte*)buf.data();
		for (int i = 0; i < height; ++i) {
			auto s = src + (src_stride * i);
			auto d = dst + (dst_stride * i);
			for (int j = 0; j < width; ++j) {
				d[0] = s[0];
				d[1] = s[1];
				d[2] = s[2];
				d[3] = s[3];
				s += 4;
				d += 4;
			}
		}
		return buf;
	}

	winrt::com_array<uint8_t> GraphicsCapture::GetActiveWindow(intptr_t hwnd, int& width, int& height) {

		std::mutex mutex;
		std::condition_variable cond;

		std::vector<byte> buf;

		m_callback = [&](ID3D11Texture2D* surface, int w, int h) {
			buf = readTexture(surface, w, h);
			width = w;
			height = h;
			cond.notify_one();
		};

		std::unique_lock<std::mutex> lock(mutex);
		if (start(hwnd)) {
			cond.wait(lock);
			stop();
		}

		return winrt::com_array<uint8_t>(buf.begin(), buf.end());
	}
}
