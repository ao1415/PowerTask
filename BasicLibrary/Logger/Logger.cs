using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Logger
{
    internal class Logger
    {
        public static BasicLogger Log { get; } = new("BasicLibrary");
    }
}
