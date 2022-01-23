using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCapt
{
    internal class Logger
    {
        public static BasicLibrary.Logger.BasicLogger Log { get; } = new("WinCapt");
    }
}
