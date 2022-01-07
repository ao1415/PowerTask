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

            Logger.Information("アプリ起動");
            _ = new TaskIcon();
            Application.Run();
            Logger.Information("アプリ終了");
        }
    }
}
