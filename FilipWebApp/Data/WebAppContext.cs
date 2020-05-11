using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FilipWebApp.Models;
using WebApp.Models;

namespace WebApp.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext (DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Reject> Rejects { get; set; }
        public DbSet<Fisher> fishers { get; set; }
    }
}
