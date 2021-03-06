﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using EntityFrameworkCore.Triggers;
using Sep3Tier3WithAuth.Entities;
using Sep3Tier3WithAuth.Helpers;

namespace Sep3Tier3WithAuth.Models
{
    public class LikeModel
    {
        public int Fisher2Id { get; set; }
        public int InteractionsId { get; set; } = 1;
    }
}
