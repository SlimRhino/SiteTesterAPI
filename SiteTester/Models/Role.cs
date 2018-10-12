using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace SiteTester.Models
{
    public class Role :IdentityRole<int>
    {
        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}
