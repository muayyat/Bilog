﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bilog.Data.Models
{
    public partial class Seeder
    {
      
        //Admin Account
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }

        //Settings
        public string AllowReg { get; set; }
        public string VerifiedReg { get; set; }
        public string AppName { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpFrom { get; set; }
        public string SmtpSecure { get; set; }
        public string SmtpAuth { get; set; }
        public string Applogo { get; set; }
        public string Connstring { get; set; }
     }
}