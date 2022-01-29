using BasicLibrary;
using BasicLibrary.Config;
using BasicLibrary.Event;
using System.Text;
using System.Text.RegularExpressions;

namespace WinCapt
{
    public class WinCapt : IPowerTaskModule
    {
        private string FolderPathTemplate { get; init; } = Config.AppSettings["WinCaptFolder"];
        private string FileNameTemplate { get; init; } = Config.AppSettings["WinCaptFormat"];

        private readonly PathUtils _pathUtils;

        public WinCapt()
        {
            _pathUtils = new PathUtils(FolderPathTemplate, FileNameTemplate);
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        void IPowerTaskModule.Initialize()
        {
            Logger.Log.Information("初期化開始");

            GlobalKeyEventInvoker.Instance.KeyDown.AddEvent(WinCapt_KeyDown);

            Logger.Log.Information("初期化終了");
        }

        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinCapt_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Alt | Keys.PrintScreen))
            {
                GraphicsCapture.SaveActiveWindow(_pathUtils);
            }
        }

    }
}
