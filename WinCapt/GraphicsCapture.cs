

using BasicLibrary;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using WinCapt.WindowsAPI;

namespace WinCapt
{
    internal class GraphicsCapture
    {
        private readonly WinRTComponent.GraphicsCapture _capture = new();

        /// <summary>
        /// アクティブウィンドウをキャプチャして保存する
        /// </summary>
        /// <param name="winCapt"></param>
        public void SaveActiveWindow(PathUtils pathUtils)
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

            byte[] data = _capture.GetActiveWindow((ulong)hwnd.ToInt64(), out int width, out int height);

            Bitmap bmp = new(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.WriteOnly, bmp.PixelFormat);
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            bmp.UnlockBits(bmpData);
            // 1ピクセルだけ切り出し
            bmp.Clone(new Rectangle(1, 1, bmp.Width - 2, bmp.Height - 2), bmp.PixelFormat).Save(filePath);
            Logger.Log.Information($"画像を保存しました:{filePath}");

            BasicLibrary.Toast.Toast.Show("画像を保存しました", new Uri(filePath));
        }

    }
}
