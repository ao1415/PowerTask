using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCron
{
    internal record ConfigJsonRecord
    {
        public string? Name { get; init; }
        public string? Explain { get; init; }
        public string? Timing { get; init; }
        public string? Path { get; init; }
        public string? Param { get; init; }
        public bool? Enable { get; init; }
    }
}
