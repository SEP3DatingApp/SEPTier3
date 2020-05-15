using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public char Gender { get; set; }
        public char SexPref { get; set; }
        [Required]
        public int Age { get; set; }
        public string Description { get; set; }
    }
}
