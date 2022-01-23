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
            BasicLogger.Initialize();

            Logger.Log.Information("�A�v���N��");

            if (Initializer.Initialize())
            {
                _ = new TaskIcon();
                Application.Run();
            }

            Logger.Log.Information("�A�v���I��");
        }
    }
}
