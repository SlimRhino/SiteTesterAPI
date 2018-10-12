using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteTester.Models;



namespace SiteTester
{
    public partial class AppContext
    {
        public DbSet<SiteModel> Sites { get; set; }

    }
}
