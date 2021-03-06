using BasicLibrary.WindowsAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Event
{
    public sealed class GlobalKeyEventInvoker
    {
        /// <summary>インスタンス</summary>
        public static GlobalKeyEventInvoker Instance { get; } = new GlobalKeyEventInvoker();

        /// <summary>キー押下イベント</summary>
        public EventManager<object, KeyEventArgs> KeyDown { get; } = new("キー押下イベント");
        /// <summary>キー離上イベント</summary>
        public EventManager<object, KeyEventArgs> KeyUp { get; } = new("キー離上イベント");

        /// <summary>フックハンドル</summary>
        private SafeHookHandle? _hookHandle = null;
        /// <summary>コールバック関数</summary>
        private readonly NativeMethods.LowLevelKeyboardProc _keyboardProc;

        private GlobalKeyEventInvoker()
        {
            _keyboardProc = HookProcedure;
            SetHook();
        }

        ~GlobalKeyEventInvoker()
        {
            Unhook();
        }

        /// <summary>
        /// グローバルキーフック登録
        /// </summary>
        private void SetHook()
        {
            if (_hookHandle == null)
            {
                IntPtr moduleHandle = NativeMethods.GetModuleHandle(null);
                _hookHandle = NativeMethods.SetWindowsHookEx((int)HookExType.WH_KEYBOARD_LL, _keyboardProc, moduleHandle, 0);
            }
        }

        /// <summary>
        /// グローバルキーフック解除
        /// </summary>
        private void Unhook()
        {
            if (_hookHandle != null)
            {
                _hookHandle.Close();
                _hookHandle = null;
            }
        }

        /// <summary>
        /// キーフックコールバック
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr HookProcedure(int nCode, IntPtr wParam, KBDLLHOOKSTRUCT lParam)
        {
            if (nCode == 0)
            {
                try
                {
                    Logger.Logger.Log.Verbose("キーフック");
                    Keys keys = (Keys)lParam.vkCode | Control.ModifierKeys;
                    if (wParam == (IntPtr)KeyboardMessage.WM_KEYDOWN || wParam == (IntPtr)KeyboardMessage.WM_SYSKEYDOWN)
                    {
                        OnKeyDownEvent(keys);
                    }
                    else if (wParam == (IntPtr)KeyboardMessage.WM_KEYUP || wParam == (IntPtr)KeyboardMessage.WM_SYSKEYUP)
                    {
                        OnKeyUpEvent(keys);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Logger.Log.Error(ex, "グローバルキーフック処理でエラー");
                }
            }

            return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        /// <summary>
        /// キー押下イベント発行
        /// </summary>
        /// <param name="keyCode">仮想キーコード</param>
        private void OnKeyDownEvent(Keys keys)
        {
            KeyDown.Invoke(this, new KeyEventArgs(keys));
        }

        /// <summary>
        /// キー離上イベント発行
        /// </summary>
        /// <param name="keyCode">仮想キーコード</param>
        private void OnKeyUpEvent(Keys keys)
        {
            KeyUp.Invoke(this, new KeyEventArgs(keys));
        }
    }

}
