using System;
using System.Collections.Generic;

namespace Bilog.Data.Models
{
    public partial class ProjectsPaged
    {
        public List<Projects> Projects { get; set; }
        public int ProjectCount { get; set; }
    }
}