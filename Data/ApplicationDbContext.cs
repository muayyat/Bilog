using System;
using System.Collections.Generic;
using System.Text;
using Bilog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Bilog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
       
        //public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BlogCategory> BlogCategory { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Categorys> Categorys { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
 }
}
