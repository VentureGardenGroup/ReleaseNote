﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReleaseNote.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : this("DefaultConnection")
        {
        }

        public ApplicationDbContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }
       

        public DbSet<Project> Projects { get; set; }
    }
}