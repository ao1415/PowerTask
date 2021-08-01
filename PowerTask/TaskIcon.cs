using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace PowerTask
{
    public partial class TaskIcon : Form
    {
        /// <summary>PowerTaskの親ウィンドウ</summary>
        public Window ParentWindow { set; get; }

        /// <summary>コンフィグウィンドウ</summary>
        private Config.MainWindow MainWindow { get; set; }

        public TaskIcon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// メニュー　設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainWindow == null || !MainWindow.IsVisible)
            {
                // コンフィグウィンドウが表示されていない場合に表示する
                MainWindow = new Config.MainWindow();
                MainWindow.Show();
            }
            else
            {
                _ = MainWindow.Activate();
            }
        }

        /// <summary>
        /// メニュー　終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow?.Close();
            ParentWindow?.Close();
        }
    }
}
