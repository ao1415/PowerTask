#include "pch.h"
#include "GraphicsCapture.h"
#include "GraphicsCapture.g.cpp"

#include "winrt/Windows.Graphics.Capture.h"
#include <Windows.Graphics.Capture.Interop.h>

namespace winrt::WinRTComponent::implementation {

	winrt::com_array<uint8_t> GraphicsCapture::GetActiveWindow(intptr_t hwnd) {

		winrt::Windows::Graphics::Capture::GraphicsCaptureItem item = nullptr;
		const auto factory = get_activation_factory<Windows::Graphics::Capture::GraphicsCaptureItem>();
		const auto interop = factory.as<IGraphicsCaptureItemInterop>();
		interop->CreateForWindow(reinterpret_cast<HWND>(hwnd), guid_of<ABI::Windows::Graphics::Capture::IGraphicsCaptureItem>(), reinterpret_cast<void**>(put_abi(item)));

		item.DisplayName();

		return winrt::com_array<uint8_t>();
	}
}
