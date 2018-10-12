using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteTester.Models
{
    public class SiteModel
    {
        public int Id { get; set; }
        [Required]
        public string URI { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;
        public DateTime LastAvailable { get; set; }
    }
}
