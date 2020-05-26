using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Models
{
    public class AddAdminModel
    {
        [Required]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Username contains ':', '.' ';', '*', '/' or '\' which are not allowed!")]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Password contains ':', '.' ';', '*', '/' or '\' which are not allowed!")]
        public string Password { get; set; }
    }
}
