using BasicLibrary;
using System.Configuration;

namespace PowerTask
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Config.Initialize(ConfigurationManager.AppSettings);
            Logger.Initialize();

            Logger.Information("�A�v���N��");

            Initialize();

            _ = new TaskIcon();
            Application.Run();
            Logger.Information("�A�v���I��");
        }

        /// <summary>
        /// ������
        /// </summary>
        private static void Initialize()
        {
            Logger.Information("�A�v���������J�n");

            new WinCron.Cron().Initialize();

            Logger.Information("�A�v���������I��");
        }
    }
}
