#pragma once

#include "Class.g.h"

namespace winrt::WinRTLibrary::implementation {
	class Class : public ClassT<Class> {
	private:

		int32_t m_value = 0;

	public:
		Class() = default;
		Class(int32_t value);

		int32_t MyProperty();
		void MyProperty(int32_t value);
		int32_t MyMethod() const {
			return m_value * 2;
		}
	};
}

namespace winrt::WinRTLibrary::factory_implementation {
	struct Class : ClassT<Class, implementation::Class> {
	};
}
