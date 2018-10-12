using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SiteTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimerController : ControllerBase
    {
        private readonly AppContext _db;

        public TimerController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> SetTimer(int seconds)
        {
            await ConsumeScopedServiceHostedService.ChangeInterval(TimeSpan.FromSeconds(seconds));
            return Ok();
        }
    }
}