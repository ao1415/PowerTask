using BasicLibrary;

namespace PowerTask
{
    public partial class TaskIcon : Form
    {
        private readonly System.Timers.Timer Timer;

        /// <summary>タイマーインターバル取得</summary>
        /// <returns></returns>
        private double GetInterval() => 1001 - DateTime.Now.Millisecond;

        public TaskIcon()
        {
            InitializeComponent();

            Timer = new System.Timers.Timer
            {
                Interval = GetInterval(),
                AutoReset = true
            };
            Timer.Elapsed += (sender, e) =>
            {
                ClockEventInvoker.Instance.RaiseEvent();
                Timer.Interval = GetInterval();
            };
            Timer.Start();

            ClockEventInvoker.Instance.AddEvent("log", (sender, e) =>
            {
                Logger.Verbose("ClockEvent");
            });
        }

        /// <summary>
        /// アプリ終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Information("終了選択");
            Timer.Stop();

            Application.Exit();
        }

        /// <summary>
        /// ログフォルダ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Information("ログフォルダ表示");

            _ = System.Diagnostics.Process.Start(Logger.FolderPath);
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
