using System;
using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Models
{
    public class AuthenticateModel
    { 
        [Required]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Username contains ':', '.' ';', '*', '/' and '\' which are not allowed!")]
        public String Username { get; set; }

        [Required]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Password contains ':', '.' ';', '*', '/' and '\' which are not allowed!")]
        public String Password { get; set; }
    }
}
