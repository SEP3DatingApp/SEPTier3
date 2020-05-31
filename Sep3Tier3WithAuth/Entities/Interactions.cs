using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sep3Tier3WithAuth.Entities
{
    public class Interactions
    {
        [Key]
        public int Id { get; set; }
        public string InteractionName { get; set; }
        public ICollection<LikeReject> LikeRejects { get; set; }
    }
}
