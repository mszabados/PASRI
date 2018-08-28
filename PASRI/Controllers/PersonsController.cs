using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PASRI.Core;
using PASRI.Core.Domain;

namespace PASRI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET /api/persons
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetPersons()
        {
            return Ok(_unitOfWork.Persons.GetAll());
        }

        // GET /api/persons/5
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(404)]
        public IActionResult GetPerson(int id)
        {
            var person = _unitOfWork.Persons.Get(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST /api/persons
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(400)]
        public IActionResult CreatePerson(Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _unitOfWork.Persons.Add(person);
            _unitOfWork.Complete();

            return Ok(person);
        }

        // PUT /api/persons/5
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePerson(int id, Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var personInDb = _unitOfWork.Persons.Get(id);
            if (personInDb == null)
                return NotFound();

            // Do nothing

            return Ok();
        }

        // DELETE /api/persons/5
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeletePerson(int id)
        {
            var person = _unitOfWork.Persons.Get(id);

            if (person == null)
                return NotFound();

            _unitOfWork.Persons.Remove(person);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
