using BasicLibrary;

namespace WinCron
{
    public class Cron
    {

        private readonly FileSystemWatcher Watcher;
        private readonly List<ConfigParse> Configs = new();

        private string FolderPath { get; } = "";
        private string FileName { get; } = "";

        public Cron()
        {

            Watcher = new();
            Watcher.NotifyFilter = NotifyFilters.LastWrite;
            Watcher.Path = FolderPath;
            Watcher.Filter = FileName;
            Watcher.Changed += new FileSystemEventHandler(Watcher_Changed);
            Watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Logger.Information("設定ファイル更新検知");

            List<ConfigJsonRecord>? jsonList;

            try
            {
                string filePath = Path.Combine(FolderPath, FileName);
                string text = File.ReadAllText(filePath);
                jsonList = System.Text.Json.JsonSerializer.Deserialize<List<ConfigJsonRecord>>(text);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "設定読み込みに失敗しました");
                return;
            }

            if (jsonList != null)
            {
                List<ConfigParse> update = new();
                foreach (var record in jsonList)
                {
                    ConfigParse? parse = ConfigParse.FromConfigJsonRecord(record);
                    if (parse != null)
                    {
                        update.Add(parse);
                    }
                }

                lock (Configs)
                {
                    Configs.Clear();
                    Configs.AddRange(update);
                }
                Logger.Information("設定ファイル反映完了");
            }
        }

        /// <summary>
        /// 定期イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cron_Clock(object sender, EventArgs e)
        {
            List<ConfigParse> execList = new();
            DateTime now = DateTime.Now;
            lock (Configs)
            {
                foreach (ConfigParse item in Configs)
                {
                    if (item.IsMatch(now))
                    {
                        execList.Add(item);
                    }
                }
            }

            foreach (var exec in execList)
            {
                ShellExecuter.Exec(exec.Path, exec.Param);
            }
        }
    }
}
