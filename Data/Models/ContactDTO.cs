using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilog.Data.Models
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Seen { get; set; }
        public DateTime? Replied { get; set; }
        public string Title { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Details { get; set; }
        public string Source { get; set; }
        public string Reply { get; set; }
        public string User { get; set; }
    }
}
