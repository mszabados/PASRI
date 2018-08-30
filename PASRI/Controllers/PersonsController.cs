﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASRI.Core;
using PASRI.Core.Domain;
using System.Collections.Generic;

namespace PASRI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET /api/Persons
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetPersons()
        {
            return Ok(_unitOfWork.Persons.GetAll());
        }

        // GET /api/Persons/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Person))]
        [ProducesResponseType(404)]
        public IActionResult GetPerson(int id)
        {
            var person = _unitOfWork.Persons.Get(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST /api/Persons
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

        // PUT /api/Persons/5
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

        // DELETE /api/Persons/5
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
