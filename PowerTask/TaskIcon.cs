using BasicLibrary;

namespace PowerTask
{
    public partial class TaskIcon : Form
    {
        private readonly System.Timers.Timer _timer;

        /// <summary>�^�C�}�[�C���^�[�o���擾</summary>
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
        /// �A�v���I���C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Log.Information("�I���I��");
            _timer.Stop();

            Application.Exit();
        }

        /// <summary>
        /// ���O�t�H���_�\��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Log.Information("���O�t�H���_�\��");

            _ = System.Diagnostics.Process.Start(BasicLogger.FolderPath);
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
