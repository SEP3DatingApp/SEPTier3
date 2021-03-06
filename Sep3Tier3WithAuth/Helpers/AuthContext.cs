﻿using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggers;
using Sep3Tier3WithAuth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sep3Tier3WithAuth.Helpers
{
    public class AuthContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AuthContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseNpgsql(Configuration["ConnectionStrings:Database"]);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Fisher> Fishers { get; set; }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<PersonSexuality> PersonSexualities { get; set; }

        public DbSet<Interactions> Interactions { get; set; }

        public DbSet<LikeReject> LikeReject { get; set; }

        public DbSet<LikePersonList> PeopleWhoMatched { get; set; }
    }
}
