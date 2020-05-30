using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Entities
{
    public class PersonSexuality
    {
        [Key]
        public int Id { get; set; }
        public string SexualityName { get; set; }
        public ICollection<Fisher> Fishers { get; set; }
    }
}
