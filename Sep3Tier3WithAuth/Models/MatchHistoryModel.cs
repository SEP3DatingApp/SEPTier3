using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sep3Tier3WithAuth.Entities;

namespace Sep3Tier3WithAuth.Models
{
    public class MatchHistoryModel
    {
        public int Fisher1Id { get; set; }
        public int Fisher2Id { get; set; }
        public int InteractionsId { get; set; } = 1;
    }
}
