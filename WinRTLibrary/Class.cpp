#include "pch.h"
#include "Class.h"
#include "Class.g.cpp"

namespace winrt::WinRTLibrary::implementation
{
	Class::Class(int32_t value) : m_value(value) {}

	int32_t Class::MyProperty() {
		return m_value;
	}

	void Class::MyProperty(int32_t value) {
		this->m_value = value;
	}
}
