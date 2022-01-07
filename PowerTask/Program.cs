using BasicLibrary;

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

            Logger.Initialize();

            Logger.Information("�A�v���N��");
            _ = new TaskIcon();
            Application.Run();
            Logger.Information("�A�v���I��");
        }
    }
}
