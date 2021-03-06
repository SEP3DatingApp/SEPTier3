﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggers;
using Sep3Tier3WithAuth.Helpers;

namespace Sep3Tier3WithAuth.Entities
{
    public class LikeReject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Fisher1Id { get; set; }
        public Fisher Fisher1 { get; set; }
        [Required]
        public int Fisher2Id { get; set; }
        public Fisher Fisher2 { get; set; }
        public int InteractionsId { get; set; }
    }
}
