using BasicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCron
{
    internal class ConfigParse
    {
        public string Name { get; init; }
        public string Path { get; init; }
        public string Param { get; init; }

        private HashSet<int> Seconds { get; init; }
        private HashSet<int> Minutes { get; init; }
        private HashSet<int> Hour { get; init; }
        private HashSet<int> Day { get; init; }
        private HashSet<int> Month { get; init; }
        private HashSet<int> Week { get; init; }

        public static ConfigParse? FromConfigJsonRecord(ConfigJsonRecord record)
        {
            if (!record.Enable.HasValue || !record.Enable.Value)
            {
                return null;
            }

            string name = record.Name ?? string.Empty;
            string path = record.Path ?? string.Empty;
            string param = record.Param ?? string.Empty;

            string[] timingSplit = record.Timing?.Split(" ") ?? Array.Empty<string>();
            if (timingSplit.Length != 6)
            {
                Logger.Log.Warning($"Timingの設定値に異常があります：[{name}]{record.Timing}");
                return null;
            }

            HashSet<int> seconds;
            HashSet<int> minutes;
            HashSet<int> hour;
            HashSet<int> day;
            HashSet<int> month;
            HashSet<int> week;
            try
            {
                seconds = GetTarget(timingSplit[0], 0, 59);
                minutes = GetTarget(timingSplit[1], 0, 59);
                hour = GetTarget(timingSplit[2], 0, 23);
                day = GetTarget(timingSplit[3], 1, 31);
                month = GetTarget(timingSplit[4], 1, 12);
                week = GetTarget(timingSplit[5], 0, 7);

                if (week.Contains(7))
                    week.Add(0);
            }
            catch (Exception)
            {
                Logger.Log.Warning($"Timingの設定値に異常があります：[{name}]{record.Timing}");
                return null;
            }

            if (string.IsNullOrEmpty(path))
            {
                Logger.Log.Warning($"Pathの設定値に異常があります：{name}");
                return null;
            }
            else if (!File.Exists(path))
            {
                Logger.Log.Warning($"Pathに指定されたファイルがありません：{name}, {path}");
                return null;
            }

            return new ConfigParse(
                name: name,
                path: path,
                param: param,
                seconds: seconds,
                minutes: minutes,
                hour: hour,
                day: day,
                month: month,
                week: week
            );
        }

        /// <summary>
        /// 対象時間取得
        /// </summary>
        /// <param name="command"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static HashSet<int> GetTarget(string command, int min, int max)
        {
            // 8-20/3 -> 8,11,14,17,20
            // 1,8-20/3 -> 1,8,11,14,17,20
            // */3 -> 0-59/3 -> 1,8,11,14,17,20

            HashSet<int> target = new();

            //ワイルドカードを[0-59]に変換
            command = command.Replace("*", $"{min}-{max}");

            string[] parts = command.Split(',');

            foreach (string range in parts)
            {
                string[] item = range.Split('/');
                string[] section = item[0].Split('-');

                if (section.Length == 1)
                {
                    Logger.Log.Verbose($"Parse:{section[0]}");
                    target.Add(int.Parse(section[0]));
                }
                else
                {
                    int d = 1;
                    if (item.Length == 2)
                    {
                        Logger.Log.Verbose($"Parse:{item[1]}");
                        d = int.Parse(item[1]);
                    }

                    Logger.Log.Verbose($"Parse:{section[0]}:{section[1]}");
                    int begin = int.Parse(section[0]);
                    int end = int.Parse(section[1]);

                    for (int t = begin; t <= end; t += d)
                    {
                        target.Add(t);
                    }
                }
            }

            Logger.Log.Verbose($"{string.Join(",", target)}");

            return target;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="param"></param>
        /// <param name="seconds"></param>
        /// <param name="minutes"></param>
        /// <param name="hour"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="week"></param>
        private ConfigParse(string name, string path, string param, HashSet<int> seconds, HashSet<int> minutes, HashSet<int> hour, HashSet<int> day, HashSet<int> month, HashSet<int> week)
        {
            Name = name;
            Path = path;
            Param = param;
            Seconds = seconds;
            Minutes = minutes;
            Hour = hour;
            Day = day;
            Month = month;
            Week = week;
        }

        /// <summary>
        /// 実行判定
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public bool IsMatch(DateTime now)
        {
            if (!Seconds.Contains(now.Second))
                return false;

            if (!Minutes.Contains(now.Minute))
                return false;

            if (!Hour.Contains(now.Hour))
                return false;

            if (!Day.Contains(now.Day))
                return false;

            if (!Month.Contains(now.Month))
                return false;

            if (!Week.Contains((int)now.DayOfWeek))
                return false;

            return true;
        }
    }
}
