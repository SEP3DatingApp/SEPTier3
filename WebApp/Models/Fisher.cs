using FilipWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Fisher: User
    {
        public string Email { get; set; }
        public char Gender { get; set; }
        public char SexPref { get; set; }
        public string PicRef { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }

    }
}
