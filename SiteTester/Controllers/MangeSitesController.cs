using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SiteTester.Models;

namespace SiteTester.Controllers
{
    [Authorize]
    [Route("api/track/manage")]
    [ApiController]
    public class MangeSitesController : ControllerBase
    {
        private readonly AppContext _db;

        public MangeSitesController(AppContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> AddSiteToDb(string q)
        {
            var splitQuery = String.IsNullOrEmpty(q) ? null : q.Split("|");

            if (splitQuery == null)
            { return BadRequest("empty request"); }

            var sites = SitesTolist(splitQuery);                   

            var user = await _db.Users.SingleAsync(u => u.UserName == HttpContext.User.Identity.Name);
            foreach (var site in sites)
            {
                if (await _db.Sites.SingleOrDefaultAsync(s => s.URI == site) == null)
                {
                    await _db.Sites.AddAsync(new SiteModel
                    {
                        URI = site.ToLower(),
                        UserId = user.Id
                    });
                }
            }
            await _db.SaveChangesAsync();

            return Ok();
        }

        
        [HttpDelete]
        public async Task<IActionResult> Delete(string q)
        {
            var splitQuery = String.IsNullOrEmpty(q) ? null : q.Split("|");

            if (splitQuery == null)
            { return BadRequest("empty request"); }

            var sites = SitesTolist(splitQuery);

            SiteModel _site = null;
            foreach (var site in sites)
            {
                _site = await _db.Sites.SingleOrDefaultAsync(s => s.URI == site);
                if (_site != null)
                {
                    _db.Sites.Remove(_site);
                }
            }
            await _db.SaveChangesAsync();
            return Ok();
        }

        public List<string> SitesTolist(string[] q)
        {
            var sites = new List<string>();
            foreach (var site in q)
            {
                if(Validation(site))
                { sites.Add(site); }
            }

            return sites;
        }

        public bool Validation (string site)
        {
            if (String.IsNullOrWhiteSpace(site))
                return false;

            var pattern = @"^([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";

            if (!Regex.IsMatch(site, pattern, RegexOptions.IgnoreCase))
                return false;

            return true;
        }
    }
}