using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FilipWebApp.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace FilipWebApp.Data
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
                    userType = "Fisher", username = "bybis",password = "dalius", email = "bybis@fsf.com", gender = 'M',sexPref = 'F',picRef = "rasr2",age = 12,isActive = true
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