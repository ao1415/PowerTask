using BasicLibrary;

namespace PowerTask
{
    public partial class TaskIcon : Form
    {
        private readonly System.Timers.Timer _timer;

        /// <summary>タイマーインターバル取得</summary>
        /// <returns></returns>
        private static double GetInterval()
        {
            return 1001 - DateTime.Now.Millisecond;
        }

        public TaskIcon()
        {
            InitializeComponent();

            _timer = new System.Timers.Timer
            {
                Interval = GetInterval(),
                AutoReset = true
            };
            _timer.Elapsed += (sender, e) =>
            {
                ClockEventInvoker.Instance.RaiseEvent();
                _timer.Interval = GetInterval();
            };

            _timer.Start();

        }

        /// <summary>
        /// アプリ終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Log.Information("終了選択");
            _timer.Stop();

            Application.Exit();
        }

        /// <summary>
        /// ログフォルダ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Log.Information("ログフォルダ表示");

            _ = System.Diagnostics.Process.Start(BasicLogger.FolderPath);
        }

        /// <summary>
        /// 最新ログ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogShowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 設定表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 設定表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ConfigToolStripMenuItem_Click(sender, e);
        }
    }
}
