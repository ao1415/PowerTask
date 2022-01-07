using BasicLibrary;

namespace PowerTask
{
    public partial class TaskIcon : Form
    {
        private readonly System.Timers.Timer Timer;

        public TaskIcon()
        {
            InitializeComponent();

            Timer = new System.Timers.Timer
            {
                Interval = TimeSpan.FromSeconds(1).TotalMilliseconds,
                AutoReset = true
            };
            Timer.Elapsed += (sender, e) => { ClockEventInvoker.Instance.RaiseEvent(); };
            Timer.Start();

            ClockEventInvoker.Instance.AddEvent("log", (sender, e) =>
            {
                DateTime date = DateTime.Now;
                Logger.Verbose("{0},ClockEvent:Start", date);
                Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                Logger.Verbose("{0},ClockEvent:End", date);
            });
            ClockEventInvoker.Instance.AddEvent("log2", (sender, e) =>
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
