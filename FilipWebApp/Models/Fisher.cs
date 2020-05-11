using FilipWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Fisher: User
    {
        public string email { get; set; }
        public char gender { get; set; }
        public char sexPref { get; set; }
        public string picRef { get; set; }
        public int age { get; set; }
        public bool isActive { get; set; }
        public string name{ get; set; }
        public string description { get; set; }

    }
}
