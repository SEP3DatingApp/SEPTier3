using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Models
{
    public class UpdateModel
    {
        [RegularExpression(@"^[^\\/:*;\)\(]+$", ErrorMessage = "Email contains ':', '.' ';', '*', '/' or '\' which are not allowed!")]
        public string Email { get; set; }
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Password contains ':', '.' ';', '*', '/' or '\' which are not allowed!")]

        public string Password { get; set; }
        public int PersonSexualityId { get; set; }
        public string PicRef { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
