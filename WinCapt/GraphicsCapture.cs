

using BasicLibrary;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using WinCapt.WindowsAPI;

namespace WinCapt
{
    internal class GraphicsCapture
    {
        /// <summary>
        /// アクティブウィンドウをキャプチャして保存する
        /// </summary>
        /// <param name="winCapt"></param>
        public static void SaveActiveWindow(PathUtils pathUtils)
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            if (hwnd == IntPtr.Zero)
            {
                Logger.Log.Warning("アクティブウィンドウが取得できませんでした");
                return;
            }

            StringBuilder title = new(1024);
            _ = NativeMethods.GetWindowTextW(hwnd, title, title.Capacity);

            string? filePath = pathUtils.GetSaveFilePath(title);
            if (filePath == null)
            {
                Logger.Log.Warning("ファイル名に異常があります");
                return;
            }

            WinRTComponent.GraphicsCapture capture = new();
            byte[] data = capture.GetActiveWindow((ulong)hwnd.ToInt64(), out int width, out int height);

            Bitmap bmp = new(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.WriteOnly, bmp.PixelFormat);
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            bmp.UnlockBits(bmpData);
            bmp.Save(filePath);

        }

    }
}
