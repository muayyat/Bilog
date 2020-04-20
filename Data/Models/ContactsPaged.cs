using System;
using System.Collections.Generic;

namespace Bilog.Data.Models
{
    public partial class ContactsPaged
    {
        public List<Contacts> Contacts { get; set; }
        public int ContactCount { get; set; }
    }
}