using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Config
{
    /// <summary>
    /// 設定ファイル
    /// </summary>
    public class Config
    {
        public static ConfigCollection AppSettings { get; private set; } = new();

        /// <summary>
        /// コンフィグ初期化
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(NameValueCollection config)
        {
            AppSettings = new(config);
            Logger.Logger.Log.Information("コンフィグ設定完了");
        }
    }
}
