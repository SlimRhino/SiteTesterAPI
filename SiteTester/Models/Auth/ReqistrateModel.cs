using System.ComponentModel.DataAnnotations;

namespace SiteTester.Models
{
    public class ReqistrateModel
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
