using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Max characters for username are 20")]
        public string Username { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Max characters for password are 20")]
        public string Password { get; set; }
        [Required]
        [StringLength(50,ErrorMessage = "Max characters for firstname are 50")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Max characters for surname are 50")]
        public string Surname { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The Gender must be 1 characters.")]
        [RegularExpression("M|F", ErrorMessage = "The Gender must be either 'M' or 'F' only.")]
        public string Gender { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The SexPref must be 1 characters.")]
        [RegularExpression("M|F|B", ErrorMessage = "The SexPref must be either 'M','F' or 'B'(For both) only.")]
        public string SexPref { get; set; }
        [Required]
        public int Age { get; set; }

        [StringLength(250, ErrorMessage = "Max characters for description are 250")]
        public string Description { get; set; }
    }
}
