﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCapt
{
    internal class Logger
    {
        public static BasicLibrary.BasicLogger Log { get; } = new("WinCapt");
    }
}