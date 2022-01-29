using BasicLibrary;
using BasicLibrary.Event;
using BasicLibrary.Logger;

namespace PowerTask
{
    public partial class TaskIcon : Form
    {
        public TaskIcon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �A�v���I���C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Log.Information("�I���I��");

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
