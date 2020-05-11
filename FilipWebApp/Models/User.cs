using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace FilipWebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        [Required]
        public string  userType{ get; set; }

        public ICollection<Match> matches { get; set; }

    }
}
