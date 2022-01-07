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
            Application.Run(new Form1());
            Logger.Information("アプリ終了");
        }
    }
}
