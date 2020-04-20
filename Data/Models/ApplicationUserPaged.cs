using System.Collections.Generic;

namespace Bilog.Data.Models
{
    public class ApplicationUserPaged
    {
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public int ApplicationUserCount { get; set; }
    }
}
