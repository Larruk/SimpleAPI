﻿using Microsoft.EntityFrameworkCore;

namespace SimpleWebAPI.Data {
    public class PersonContext : DbContext {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
