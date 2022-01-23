using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTask
{
    internal class Logger
    {
        public static BasicLibrary.BasicLogger Log { get; } = new("PowerTask");
    }
}
