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
