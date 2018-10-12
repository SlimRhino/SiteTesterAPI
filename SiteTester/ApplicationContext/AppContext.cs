using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteTester.Models;


namespace SiteTester
{
    public partial class AppContext : IdentityDbContext<User,Role, int>
    {
       public AppContext(DbContextOptions<AppContext> options) :base(options)
        {
            Database.EnsureCreated();           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);          
        }
    }
}
