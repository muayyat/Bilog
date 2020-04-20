using System;
using System.Collections.Generic;

namespace Bilog.Data.Models
{
    public partial class LogsPaged
    {
        public List<Logs> Logs { get; set; }
        public int LogCount { get; set; }
    }
}