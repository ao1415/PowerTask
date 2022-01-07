using BasicLibrary;

namespace PowerTask
{
    public partial class TaskIcon : Form
    {
        private readonly System.Timers.Timer Timer;

        /// <summary>�^�C�}�[�C���^�[�o���擾</summary>
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
        /// �A�v���I���C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Information("�I���I��");
            Timer.Stop();

            Application.Exit();
        }

        /// <summary>
        /// ���O�t�H���_�\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Information("���O�t�H���_�\��");

            _ = System.Diagnostics.Process.Start(Logger.FolderPath);
        }

        /// <summary>
        /// �ŐV���O�\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogShowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �ݒ�\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �ݒ�\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ConfigToolStripMenuItem_Click(sender, e);
        }
    }
}
