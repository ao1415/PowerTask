using BasicLibrary;

namespace PowerTask
{
    internal class Initializer
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        public static bool Initialize()
        {
            Logger.Information("アプリ初期化開始");

            try
            {
                Initialize<WinCron.WinCron>();
                Initialize<WinCapt.WinCapt>();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "アプリ初期化失敗");
                return false;
            }

            Logger.Information("アプリ初期化終了");

            return true;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private static void Initialize<T>() where T : IPowerTaskModule, new()
        {
            new T().Initialize();
        }
    }
}
