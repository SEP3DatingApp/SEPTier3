using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [InverseProperty("Fisher1")]
        public ICollection<LikeReject> Fishers1 { get; set; }
        [InverseProperty("Fisher2")]
        public ICollection<LikeReject> Fishers2 { get; set; }
    }
}
