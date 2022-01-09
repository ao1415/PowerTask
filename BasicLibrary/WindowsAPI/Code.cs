using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.WindowsAPI
{
    internal enum HookExType : int
    {
        WH_KEYBOARD_LL = 13,
    }

    [Flags]
    internal enum KeyboardMessage : uint
    {
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
    }

}
