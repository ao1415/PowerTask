

using BasicLibrary;

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
            WinRTLibrary.Class cls = new(100);
            Logger.Debug(cls.MyProperty.ToString());
            Logger.Debug(cls.MyMethod().ToString());
            cls.MyProperty = 200;
            Logger.Debug(cls.MyProperty.ToString());
            Logger.Debug(cls.MyMethod().ToString());
            //IntPtr handle = NativeMethods.GetForegroundWindow();
            //if (handle != IntPtr.Zero)
            //{
            //    WindowId windowId = new() { Value = (ulong)handle.ToInt32() };

            //    var factory = WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));
            //    var interop = (IGraphicsCaptureItemInterop)factory;
            //    var iid = typeof(GraphicsCaptureItem).GetInterface("IGraphicsCaptureItem").GUID;
            //    var pointer = interop.CreateForWindow(hWnd, ref iid);
            //    var capture = Marshal.GetObjectForIUnknown(pointer) as GraphicsCaptureItem;
            //    Marshal.Release(pointer);

            //    GraphicsCaptureItem item = GraphicsCaptureItem.TryCreateFromWindowId(windowId);

            //    _ = NativeMethods.GetWindowTextA(handle, out StringBuilder title, 255);
            //    string filePath = winCapt.GetSaveFilePath(title.ToString());

            //    CanvasBitmap canvas = await GetCapture(item);
            //    await canvas.SaveAsync(filePath);
            //}
        }

        /// <summary>
        /// 指定されたアイテムをキャプチャする
        /// </summary>
        /// <param name="item">キャプチャ対象</param>
        /// <returns>キャプチャキャンバス</returns>
        //private static async Task<CanvasBitmap> GetCapture(GraphicsCaptureItem item)
        //{
        //    var tcs = new TaskCompletionSource<CanvasBitmap>();

        //    IDirect3DDevice canvasDevice = new CanvasDevice();

        //    Direct3D11CaptureFramePool framePool = Direct3D11CaptureFramePool.CreateFreeThreaded(canvasDevice, DirectXPixelFormat.R16G16B16A16Float, 1, item.Size);
        //    framePool.FrameArrived += (sender, args) =>
        //    {
        //        using var frame = framePool.TryGetNextFrame();
        //        CanvasBitmap canvas = CanvasBitmap.CreateFromDirect3D11Surface((ICanvasResourceCreator)canvasDevice!, frame.Surface);
        //        tcs.TrySetResult(canvas);
        //    };

        //    GraphicsCaptureSession session = framePool.CreateCaptureSession(item);
        //    session.StartCapture();

        //    return await tcs.Task;
        //}

    }
}
