using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SiteTester.Models
{
    public class TokenViewModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public DateTime ATokenExpiration { get; set; }
    }
}
