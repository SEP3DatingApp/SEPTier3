using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Sep3Tier3WithAuth.Models;

namespace Sep3Tier3WithAuth.Entities
{
    public class Fisher : User
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int PersonSexualityId { get; set; }
        public string PicRef { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
