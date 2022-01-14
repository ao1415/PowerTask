

using BasicLibrary;
using BasicLibrary.WindowsAPI;
using System.Text;

using Windows.Graphics.Capture;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI;

namespace WinCapt
{
    internal class GraphicsCapture
    {
        private readonly WinRTLibrary.GraphicsCapture _capture = new();

        /// <summary>
        /// アクティブウィンドウをキャプチャして保存する
        /// </summary>
        /// <param name="winCapt"></param>
        public async void SaveActiveWindow(WinCapt winCapt)
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            if (hwnd != IntPtr.Zero)
            {
                //_ = NativeMethods.GetWindowTextW(hwnd, out StringBuilder title, 255);
                
                var buffer = _capture.GetWindowCapture((long)hwnd);
                if (buffer != null)
                {
                    //string filePath = winCapt.GetSaveFilePath(title.ToString());
                    string filePath = winCapt.GetSaveFilePath("test");
                    Bitmap bmp = new(new MemoryStream(buffer));


                    bmp.Save(filePath);
                }
            }
        }

    }
}
