using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sep3Tier3WithAuth.Entities
{
    public class LikePersonList
    {
        [Key] public int Id { get; set; }
        [Required] public int Fisher1Id { get; set; }
        public Fisher Fisher1 { get; set; }
        [Required] public int Fisher2Id { get; set; }
        public Fisher Fisher2 { get; set; }
    }
}
