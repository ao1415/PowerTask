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
        /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getforegroundwindow">
        /// フォアグラウンドウィンドウのハンドルを取得する
        /// </see>
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtexta">
        /// ウィンドウのタイトルバーを取得する
        /// </see>
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="lpString">キストの受信バッファ</param>
        /// <param name="nMaxCount">バッファーにコピーする最大文字数</param>
        /// <returns>コピーされた文字列の長さ</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowTextA(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr), Out] out StringBuilder lpString, int nMaxCount);
    }
}
