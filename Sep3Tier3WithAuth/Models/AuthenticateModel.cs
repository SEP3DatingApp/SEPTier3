using System;
using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Models
{
    public class AuthenticateModel
    { 
        [Required]
        public String Username { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
