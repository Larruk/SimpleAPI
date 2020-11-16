
using SimpleWebAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebAPI.Services {
    public interface IPersonRepository {
        List<Person> GetAll();
        void Add(Person p);

    }

    public class PersonRepository : IPersonRepository {
        private readonly PersonContext personContext;

        public PersonRepository(PersonContext context) {
            personContext = context;
        }

        /// <summary>
        /// Grabs all people in the database with an optional query
        /// </summary>
        /// <param name="query">Optional, defaults to empty string</param>
        /// <returns></returns>
        public List<Person> GetAll() {
            return personContext.Persons.ToList();
        }

        /// <summary>
        /// Add a person to the db
        /// </summary>
        /// <param name="p">Person to add</param>
        public void Add(Person p) {
            personContext.Persons.Add(p);
            personContext.SaveChanges();
        }
    }
}