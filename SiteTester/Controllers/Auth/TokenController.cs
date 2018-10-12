using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteTester.Models;
using System.Security.Claims;

namespace SiteTester.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        readonly SignInManager<User> _signInManager;
        readonly UserManager<User> _userManager;
        readonly IConfiguration _configuration;

        public TokenController (SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody]LoginViewModel login)
        {
            string error = "Invalid e-mail address and/or password";
            
            if (!ModelState.IsValid)
            {
                return BadRequest(error);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return BadRequest(error);
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest(error);
            }

            var result = _signInManager.PasswordSignInAsync(user, login.Password, true, false);
            if(!result.Result.Succeeded)
            {
                return BadRequest(error);
            }

            var token = await GenerateToken(user);

            return Ok(token);
        }

        /// <summary>
        /// Создание токена авторизации для доступа к api
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Объект токена</returns>
        private async Task<TokenViewModel> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Authentication:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtIssue"],
                audience: _configuration["Authentication:JwtAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );
            return new TokenViewModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ATokenExpiration = expires
            };
        }
    }

    
}