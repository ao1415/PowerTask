﻿using BasicLibrary;

namespace WinCron
{
    public class WinCron : IPowerTaskModule
    {
        private readonly FileSystemWatcher _watcher = new();
        private readonly List<ConfigParse> _configs = new();

        private string FolderPath { get; init; } = Config.AppSettings["ConfigFolder"];
        private string FileName { get; init; } = Config.AppSettings["WinCronJsonFile"];

        /// <summary>
        /// 初期化処理
        /// </summary>
        void IPowerTaskModule.Initialize()
        {
            Logger.Information("[WinCron]初期化開始");
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            string filePath = Path.Combine(FolderPath, FileName);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Path = FolderPath;
            _watcher.Filter = FileName;
            _watcher.Changed += new FileSystemEventHandler(Watcher_Changed);
            _watcher.EnableRaisingEvents = true;

            ReadConfig();

            ClockEventInvoker.Instance.AddEvent("WinCron", WinCron_Clock);

            Logger.Information("[WinCron]初期化終了");
        }

        /// <summary>
        /// ファイル変更監視
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                Logger.Information("[WinCron]設定ファイル更新検知");
                ReadConfig();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "[WinCron]");
            }
        }

        /// <summary>
        /// 定期イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinCron_Clock(object? sender, EventArgs e)
        {
            try
            {
                Logger.Verbose("[WinCron]");

                List<ConfigParse> execList = new();
                DateTime now = DateTime.Now;
                lock (_configs)
                {
                    foreach (ConfigParse item in _configs)
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
            catch (Exception ex)
            {
                Logger.Error(ex, "[WinCron]");
            }
        }

        /// <summary>
        /// 設定ファイル読み込み
        /// </summary>
        private void ReadConfig()
        {
            Logger.Information("[WinCron]設定ファイル読み込み開始");

            List<ConfigJsonRecord>? jsonList;
            try
            {
                string text = string.Empty;

                using (FileStream fs = new(Path.Combine(FolderPath, FileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using TextReader reader = new StreamReader(fs);
                    text = reader.ReadToEnd();
                }

                jsonList = System.Text.Json.JsonSerializer.Deserialize<List<ConfigJsonRecord>>(text);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "[WinCron]設定読み込みに失敗しました");
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

                lock (_configs)
                {
                    _configs.Clear();
                    _configs.AddRange(update);
                }
                Logger.Information("[WinCron]設定ファイル反映完了");
            }

            Logger.Information("[WinCron]設定ファイル読み込み終了");
        }
    }
}
