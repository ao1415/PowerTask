using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinCapt.WindowsAPI
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
        public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowrect">
        /// ウィンドウの領域を取得する
        /// </see>
        /// </summary>
        /// <returns>コピーされた文字列の長さ</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        /// <summary>
        /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-printwindow">
        /// ウィンドウの画像を取得する
        /// </see>
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

        /// <summary>
        /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getdc">
        /// ウィンドウのクライアント領域を取得する
        /// </see>
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getdc">
        /// ウィンドウの非クライアント領域を取得する
        /// </see>
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        /// <see href="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-releasedc">
        /// ウィンドウのクライアント領域を解放する
        /// </see>
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        /// <summary>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-bitblt">
        /// ウィンドウをキャプチャする
        /// </see>
        /// </summary>
        /// <returns></returns>
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop);

    }
}
