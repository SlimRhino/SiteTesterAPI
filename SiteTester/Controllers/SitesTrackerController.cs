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
    [Route("api/track")]
    [ApiController]
    public class SitesTrackerController : ControllerBase
    {
        private readonly AppContext _db;

        public SitesTrackerController(AppContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _db.Sites.Select(s => new SiteStatus
            {
                Name = s.URI,
                IsAvailable = s.IsAvailable,
                LastAvailable = s.LastAvailable
            }).ToListAsync()
            );
        }
    }
}