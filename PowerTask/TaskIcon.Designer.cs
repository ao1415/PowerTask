namespace PowerTask
{
    partial class TaskIcon
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskIcon));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LogOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.ContextMenu;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "PowerTask";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LogOpenToolStripMenuItem,
            this.LogShowToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ConfigToolStripMenuItem,
            this.toolStripMenuItem2,
            this.ExitToolStripMenuItem});
            this.ContextMenu.Name = "contextMenuStrip1";
            this.ContextMenu.Size = new System.Drawing.Size(137, 104);
            // 
            // LogOpenToolStripMenuItem
            // 
            this.LogOpenToolStripMenuItem.Name = "LogOpenToolStripMenuItem";
            this.LogOpenToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.LogOpenToolStripMenuItem.Text = "ログフォルダ...";
            this.LogOpenToolStripMenuItem.Click += new System.EventHandler(this.LogOpenToolStripMenuItem_Click);
            // 
            // LogShowToolStripMenuItem
            // 
            this.LogShowToolStripMenuItem.Name = "LogShowToolStripMenuItem";
            this.LogShowToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.LogShowToolStripMenuItem.Text = "ログ表示...";
            this.LogShowToolStripMenuItem.Click += new System.EventHandler(this.LogShowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.ConfigToolStripMenuItem.Text = "設定...";
            this.ConfigToolStripMenuItem.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.ExitToolStripMenuItem.Text = "終了...";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // TaskIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "TaskIcon";
            this.Text = "Form1";
            this.ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NotifyIcon NotifyIcon;
        private ToolStripMenuItem LogOpenToolStripMenuItem;
        private ToolStripMenuItem ConfigToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem LogShowToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem2;
        private ContextMenuStrip ContextMenu;
    }
}