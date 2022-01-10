using BasicLibrary;
using System.Text;
using System.Text.RegularExpressions;

namespace WinCapt
{
    public class WinCapt : IPowerTaskModule
    {
        private const string TagVideo = "%video%";
        private const string TagPicture = "%picture%";

        private const string TagTitle = "%title%";
        private const string TagDate = "%date.*%";

        private string FolderPathTemplate { get; init; } = Config.AppSettings["WinCaptFolder"];
        private string FileNameTemplate { get; init; } = Config.AppSettings["WinCaptFormat"];

        private readonly string _folderPath;
        private readonly string _fileName;

        public WinCapt()
        {
            _folderPath = GetFolderPath();
            _fileName = GetFileName();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        void IPowerTaskModule.Initialize()
        {
            Logger.Information("[WinCapt]初期化開始");

            GlobalKeyEventInvoker.Instance.KeyDown += WinCapt_KeyDown;

            Logger.Information("[WinCapt]初期化終了");
        }

        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WinCapt_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Alt | Keys.PrintScreen))
            {
                GraphicsCapture.SaveActiveWindow(this);
            }
        }

        /// <summary>
        /// 保存ファイルパス取得
        /// </summary>
        /// <param name="windowTitle">ウィンドウタイトル</param>
        /// <returns></returns>
        public string GetSaveFilePath(string windowTitle)
        {
            char[] invalidch = Path.GetInvalidFileNameChars();

            StringBuilder title = new(windowTitle);
            foreach (char ch in invalidch)
            {
                title.Replace(ch.ToString(), string.Empty);
            }

            string fileName = string.Format(_fileName, title.ToString(), DateTime.Now);

            return Path.Combine(_folderPath, fileName);
        }

        /// <summary>
        /// 文字列変換
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        private static string Replace(string input, string pattern, string replacement) => Regex.Replace(input, pattern, replacement, RegexOptions.IgnoreCase);

        /// <summary>
        /// フォルダ名変換
        /// </summary>
        /// <returns></returns>
        private string GetFolderPath()
        {
            string path = FolderPathTemplate;
            path = Replace(path, TagVideo, System.Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            path = Replace(path, TagPicture, System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

            return path;
        }
        /// <summary>
        /// ファイル名変換
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            string name = FileNameTemplate;
            name = Replace(name, TagTitle, "{0}");
            Match dateMatch = Regex.Match(name, TagDate);
            if (dateMatch.Success)
            {
                string format;

                string value = dateMatch.Value;
                int index = value.IndexOf(':');
                if (0 <= index)
                {
                    format = "{1:" + value[index..(value.Length - 1)] + "}";
                }
                else
                {
                    format = "{1}";
                }
                name = Replace(name, TagDate, format);
            }

            return name;
        }

    }
}
