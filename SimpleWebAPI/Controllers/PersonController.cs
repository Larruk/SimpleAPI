using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebAPI.Services;

namespace SimpleWebAPI.Controllers {
    [ApiController]
    [Route("people")]
    public class PersonController : ControllerBase {
        private readonly ILogger<PersonController> _logger;
        private IPersonRepository _personRepository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository repository) {
            _logger = logger;
            _personRepository = repository;
        }

        /// <summary>
        ///  Fetch all the people in the database, with an optional search query
        /// </summary>
        /// <param name="query">Defaults to blank string if not provided, uses a Contains() search for first/last name.</param>
        /// <returns code="200">Successfully retrieved all people in the database</returns>
        /// <returns code="500">Something went wrong during setup or when fetching of people</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(500)]
        public List<Person> Get(string query = null) {            
            List<Person> people = this._personRepository.GetAll();

            if (!string.IsNullOrEmpty(query)) {
                return people.Where(x =>
                   x.FirstName.ToLower().Contains(query) || x.LastName.ToLower().Contains(query)
                ).ToList();
            }

            return people;
        }

        /// <summary>
        /// Add a person to the database
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public StatusCodeResult Create(Person person) {
            try {
                this._personRepository.Add(person);
                return StatusCode(201);
            } catch (Exception ex) {
                return StatusCode(500);
            }
        }
    }
}
