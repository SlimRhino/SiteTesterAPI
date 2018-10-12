using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SiteTester.Models;

namespace SiteTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public RegistrateController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Registrate([FromBody] ReqistrateModel regData)
        {
            if(!ModelState.IsValid)
            { return BadRequest(ModelState); }

            User user = await _userManager.FindByEmailAsync(regData.Email);
            if (user != null)
            { return BadRequest("User with that email already exists"); }

            user = new User
            {
                
                UserName = regData.UserName,
                Email = regData.Email,
                EmailConfirmed = true,
                LockoutEnabled = true,
            };
            var regResult = _userManager.CreateAsync(user, regData.Password);
            if (!regResult.Result.Succeeded)
            { return BadRequest(regResult.Result.Errors); }

            await _userManager.AddToRoleAsync(user, "admin");

            return Ok();
        }
    }
}