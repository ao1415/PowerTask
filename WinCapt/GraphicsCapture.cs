

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
        /// <summary>
        /// アクティブウィンドウをキャプチャして保存する
        /// </summary>
        /// <param name="winCapt"></param>
        public static async void SaveActiveWindow(WinCapt winCapt)
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            if (hwnd != IntPtr.Zero)
            {
                GraphicsCapturePicker picker=new();
                GraphicsCaptureItem item = await picker.PickSingleItemAsync();
                //WindowId id = new((ulong)hwnd);
                //GraphicsCaptureItem item = GraphicsCaptureItem.TryCreateFromWindowId(id);

                _ = NativeMethods.GetWindowTextA(hwnd, out StringBuilder title, 255);
                string filePath = winCapt.GetSaveFilePath(title.ToString());

            }
        }

    }
}
