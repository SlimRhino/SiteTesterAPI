using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SiteTester.Models;

namespace SiteTester
{   
    /// <summary>
    /// Тестовый заполнитель Базы
    /// </summary>
    public static class DBExtention
    {
        public static UserManager<User> UserManager { get; set; }
        public static RoleManager<Role> RoleManager { get; set; }

        public static void EnsureSeeded(this AppContext context)
        {
            AddRole(context);
            AddUser(context);
            AddSites(context);
        }

        public static void AddRole(AppContext context)
        {
            if (RoleManager.RoleExistsAsync("admin").GetAwaiter().GetResult() == false)
            {
                RoleManager.CreateAsync(new Role("admin")).GetAwaiter().GetResult();
            }
        }

        public static void AddUser(AppContext context)
        {
            if (UserManager.FindByEmailAsync("rhinoseros@mail.ru").GetAwaiter().GetResult() == null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@mail.ru",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };
                UserManager.CreateAsync(user, "adminNumber1").GetAwaiter().GetResult();
                var u = UserManager.FindByEmailAsync("admin@mail.ru").GetAwaiter().GetResult();
                UserManager.AddToRoleAsync(u, "admin").GetAwaiter().GetResult();
            }       
        }
        
        public static void AddSites(AppContext context)
        {
            if(context.Sites.ToList().Count == 0)
            {
                List<string> sites = new List<string> { "www.google.ru", "www.yandex.ru", "www.habr.ru" };
                foreach (var site in sites)
                {
                    context.Sites.Add(new SiteModel
                    {
                        URI = site,
                        UserId = 1,
                        IsAvailable = true,
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
