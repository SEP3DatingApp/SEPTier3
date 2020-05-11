using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FilipWebApp.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Data
{ 
    public class DbInitializer
    {

        public static void Initializer(WebAppContext context)
        {
            context.Database.EnsureCreated();
            SeedUsers(context);
        }
        private static void SeedUsers(WebAppContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            var fishers = new Fisher[]
            {
                new Fisher()
                {
                    Username = "bybis",Password = "ffsss", Email = "bybis@fsf.com", Gender = 'M',SexPref = 'F',PicRef = "rasr2",Age = 12,IsActive = true
                },
            };

            foreach (Fisher fisher in fishers)
            {
                context.Add(fisher);
            }
            context.SaveChanges();
        }
    }
}