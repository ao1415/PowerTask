#pragma once

#include "GraphicsCapture.g.h"

namespace winrt::WinRTComponent::implementation
{
    class GraphicsCapture : public GraphicsCaptureT<GraphicsCapture>{
    public:
        GraphicsCapture() = default;

        winrt::com_array<uint8_t> GetActiveWindow(intptr_t hwnd);

    };
}

namespace winrt::WinRTComponent::factory_implementation
{
    class GraphicsCapture : public GraphicsCaptureT<GraphicsCapture, implementation::GraphicsCapture>{
    };
}
