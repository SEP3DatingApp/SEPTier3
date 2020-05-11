using FilipWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Match
    {
        
       

        public User User { get; set; }
        public int UserId { get; set; }
        public int MatchId { get; set; }
    }
}
