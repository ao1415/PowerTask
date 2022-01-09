using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.WindowsAPI
{
    internal class SafeHookHandle : SafeHandle
    {
        public SafeHookHandle() : base(IntPtr.Zero, true) { }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
            {
                return NativeMethods.UnhookWindowsHookEx(this);
            }
            return true;
        }
    }
}
