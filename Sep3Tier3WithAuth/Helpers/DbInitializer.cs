using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sep3Tier3WithAuth.Entities;

namespace Sep3Tier3WithAuth.Helpers
{
    public class DbInitializer
    {
        public static void Initializer(AuthContext context)
        {
            context.Database.EnsureCreated();

            if (!context.PersonSexualities.Any())
            {
                var personSexualities = new PersonSexuality[]
                {
                    new PersonSexuality
                    {
                        Id = 1, SexualityName = "Straight"
                    },
                    new PersonSexuality
                    {
                        Id = 2, SexualityName = "Homosexual"
                    },
                    new PersonSexuality
                    {
                        Id = 3, SexualityName = "Bi-Sexual"
                    },
                };

                foreach (PersonSexuality ps in personSexualities)
                {
                    context.Add(ps);
                }
                context.SaveChanges();
            }
            else if (!context.Fishers.Any())
            {
                var fishers = new Fisher[]
                {
                    new Fisher
                    {
                        Id = 2,Age = 19,Description = "FunnyDays",Email = "myemail@email.com",FirstName = "Chris",Gender = "M",IsActive = true,PersonSexualityId = 1
                    },
                    new Fisher
                    {
                        Id = 3,Age = 20,Description = "FunnyDays2",Email = "myemail2@email.com",FirstName = "Peter",Gender = "M",IsActive = true,PersonSexualityId = 2
                    },
                    new Fisher
                    {
                        Id = 4,Age = 21,Description = "FunnyDays3",Email = "myemail3@email.com",FirstName = "Andrew",Gender = "M",IsActive = true,PersonSexualityId = 3
                    },
                    new Fisher
                    {
                        Id = 6,Age = 22,Description = "FunnyDays4",Email = "myemail4@email.com",FirstName = "Keisha",Gender = "F",IsActive = true,PersonSexualityId = 1
                    },
                    new Fisher
                    {
                        Id = 7,Age = 23,Description = "FunnyDays5",Email = "myemail5@email.com",FirstName = "Tasha",Gender = "F",IsActive = true,PersonSexualityId = 2
                    },
                    new Fisher
                    {
                        Id = 8,Age = 24,Description = "FunnyDays6",Email = "myemail6@email.com",FirstName = "Maria",Gender = "F",IsActive = true,PersonSexualityId = 3
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
}
