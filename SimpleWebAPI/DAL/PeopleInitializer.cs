using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebAPI.Data;

namespace SimpleWebAPI.DAL {
    public static class PeopleInitializer {
        public static void Seed(IServiceProvider serviceProvider) {
            using (var context = new PersonContext(serviceProvider.GetRequiredService<DbContextOptions<PersonContext>>())) {
                if (context.Persons.Any()) {
                    // Already seeded
                    return;
                }

                context.AddRange(
                    new Person { FirstName = "Laz", LastName = "Padron", Address = "123 Rainbow Road", Age = 29, Interests = "Video Games" },
                    new Person { FirstName = "Baxter", LastName = "Padron", Address = "123 Rainbow Road", Age = 9, Interests = "Barking" },
                    new Person { FirstName = "Jenny", LastName = "Padron", Address = "123 Rainbow Road", Age = 26, Interests = "Roller Derby" },
                    new Person { FirstName = "Frodo", LastName = "Baggins", Address = "Road to Mordor", Age = 25, Interests = "Whining" },
                    new Person { FirstName = "Bilbo", LastName = "Baggins", Address = "Road to Mordor", Age = 55, Interests = "Jump Scares" },
                    new Person { FirstName = "Johnny", LastName = "Smith", Address = "1 Real Road", Age = 36, Interests = "Drinking" },
                    new Person { FirstName = "Tina", LastName = "Tiger", Address = "Jungle", Age = 13, Interests = "Growling" }
                );
                context.SaveChanges();
            }
        }
    }
}
