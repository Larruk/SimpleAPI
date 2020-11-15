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
                    new Person { FirstName = "Jenny", LastName = "Padron", Address = "123 Rainbow Road", Age = 26, Interests = "Roller Derby" }
                );
                context.SaveChanges();
            }
        }
    }
}
