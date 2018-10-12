using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebTour.Models.Clients;

namespace WebTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AppContext _db;

        public ClientController(AppContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _db.Clients.SingleOrDefaultAsync(c => c.Id == id);

            if (result == null)
                { return BadRequest("no client with such id"); }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList(string q )
        {
            string Query = $"%{q?.ToLower()}%";
            return Ok(await _db.Clients
                .Where(c => String.IsNullOrEmpty(q) ||
                    EF.Functions.Like(c.Name.ToLower(), Query) ||
                    EF.Functions.Like(c.NameLat.ToLower(), Query) ||
                    EF.Functions.Like(c.Patronomic.ToLower(), Query) ||
                    EF.Functions.Like(c.PatronomicLat.ToLower(), Query) ||
                    EF.Functions.Like(c.Surname.ToLower(), Query) ||
                    EF.Functions.Like(c.SurnameLat.ToLower(), Query) ||
                    EF.Functions.Like(c._Emails, Query) ||
                    EF.Functions.Like(c._Phones, Query) ||
                    EF.Functions.Like(c._PassportGen.ToLower(), Query) ||
                    EF.Functions.Like(c._PassportInter.ToLower(), Query)
                ).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Client client)
        {
            if(!ModelState.IsValid)
            { return BadRequest(ModelState); }
            await Task.WhenAll(_db.Clients.AddAsync(client), _db.SaveChangesAsync());
            
            return Ok();
        }

        public static string PrepareQuery(string q)
        {
            q = q.Select();
        }
    }
}