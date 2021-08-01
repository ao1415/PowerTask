using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PowerTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Timers.Timer timer;
        private readonly TimeSpan interval = TimeSpan.FromSeconds(double.Parse(PowerTask.Properties.Resources.TimerInterval));

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BasicLibrary.LogUtil.Information("起動");

            new TaskIcon().ParentWindow = new NoneWindow();

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = interval.TotalMilliseconds - DateTime.Now.Millisecond + 1;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //BasicLibrary.LogUtil.Trace("");
            timer.Interval = interval.TotalMilliseconds - DateTime.Now.Millisecond + 1;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            timer.Stop();

            base.OnExit(e);
        }

    }
}
