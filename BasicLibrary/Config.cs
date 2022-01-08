using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
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
        }
    }

    /// <summary>
    /// 設定コレクション
    /// </summary>
    public class ConfigCollection
    {
        private readonly NameValueCollection Settings = new();

        public ConfigCollection()
        {

        }
        public ConfigCollection(NameValueCollection config)
        {
            Settings = config;
        }

        public string this[string name]
        {
            get
            {
                return Settings[name] ?? string.Empty;
            }
        }
    }

}
