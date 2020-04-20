using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilog.Data.Models
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectSubTitle { get; set; }
        public string ProjectClient { get; set; }
        public string Link { get; set; }
        public string ELink { get; set; }
        public string ProjectContent { get; set; }
        public string ProjectUserName { get; set; }
        public string Banner { get; set; }

        public string ProjectCategory { get; set; }
    }
}
