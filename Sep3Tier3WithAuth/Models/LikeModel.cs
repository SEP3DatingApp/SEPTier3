using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sep3Tier3WithAuth.Entities;
using Sep3Tier3WithAuth.Helpers;

namespace Sep3Tier3WithAuth.Models
{
    public class LikeModel
    {
        public int? Fisher1Id { get; set; }
        public int? Fisher2Id { get; set; }
        public int? InteractionsId { get; set; } = 1;
    }
}
