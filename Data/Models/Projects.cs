﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bilog.Data.Models
{
    public partial class Projects
    {
       
        [Key]
        public int ProjectId { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectSubTitle { get; set; }
        public string ProjectClient { get; set; }
      //  [Key]
        public string Link { get; set; }
        public string ELink { get; set; }
        public string ProjectContent { get; set; }
         public string ProjectUserName { get; set; }
        public string Banner { get; set; }

        public string ProjectCategory { get; set; }
    }
}