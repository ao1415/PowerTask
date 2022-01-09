using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.WindowsAPI
{
    internal class NativeMethods
    {
        /// <summary>
        /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-setwindowshookexa">
        /// グローバルキーフック登録
        /// </see>
        /// </summary>
        /// <param name="idHook">フックのタイプ<para>WH_KEYBOARD_LL（13）固定</para></param>
        /// <param name="lpfn">フックプロシージャへのポインタ</param>
        /// <param name="hMod">lpfnパラメータが指すフックプロシージャを含むDLLへのハンドル</param>
        /// <param name="dwThreadId">フックプロシージャが関連付けられるスレッドの識別子</param>
        /// <returns>ハンドル</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeHookHandle SetWindowsHookEx(int idHook, [MarshalAs(UnmanagedType.FunctionPtr)] LowLevelKeyboardProc lpfn, IntPtr hmod, uint dwThreadId);

        /// <summary>
        /// <see href="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)">
        /// グローバルキーフックに登録するコールバック
        /// </see>
        /// </summary>
        /// <param name="nCode">メッセージの処理方法を決定するために使用するコード<para>HC_ACTION（0）固定</para></param>
        /// <param name="wParam">キーボードメッセージの識別子</param>
        /// <param name="lParam">フックのタイプによって異なる</param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, [MarshalAs(UnmanagedType.LPStruct), In] KBDLLHOOKSTRUCT lParam);

        /// <summary>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex">
        /// フック情報を次のフックプロシージャに渡す
        /// </see>
        /// </summary>
        /// <param name="hhk">このパラメーターは無視される</param>
        /// <param name="nCode">現在のフックプロシージャに渡されるフックコード</param>
        /// <param name="wParam">フックのタイプによって異なる</param>
        /// <param name="lParam">フックのタイプによって異なる</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, [MarshalAs(UnmanagedType.LPStruct), In] KBDLLHOOKSTRUCT lParam);

        /// <summary>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex">
        /// グローバルキーフック解除
        /// </see>
        /// </summary>
        /// <param name="hhk">取り外すフックのハンドル</param>
        /// <returns>成功可否</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(SafeHandle hhk);

        /// <summary>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlea">
        /// 指定されたモジュールのモジュールハンドルを取得する
        /// </see>
        /// </summary>
        /// <param name="lpModuleName">ロードされたモジュールの名前</param>
        /// <returns>モジュールのハンドル</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr), In] string? lpModuleName);

    }
}
