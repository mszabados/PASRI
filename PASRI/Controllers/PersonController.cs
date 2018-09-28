using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASRI.API.Core;
using PASRI.API.Core.Domain;
using PASRI.API.Dtos;

namespace PASRI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all persons
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonDto>))]
        public IActionResult GetPersons()
        {
            return Ok(_unitOfWork.Persons.GetAll().Select(_mapper.Map<Person, PersonDto>));
        }

        /// <summary>
        /// Get a single person by its unique person id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(404)]
        public IActionResult GetPerson(int id)
        {
            var person = _unitOfWork.Persons.Get(id);

            if (person == null)
                return NotFound();

            return Ok(_mapper.Map<Person, PersonDto>(person));
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="payload">A data transformation object representing the person</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(PersonDto))]
        public IActionResult CreatePerson(PersonDto payload)
        {
            var person = new Person();
            try
            {
                person = ValidatePayloadCreatePerson(payload);
            }
            catch (ApplicationException e)
            {
                return StatusCode(new BadRequestResult().StatusCode, e.Message);
            }

            var personInDb = _unitOfWork.Persons.Get(person.Id);
            if (personInDb != null)
                return new ConflictResult();

            _unitOfWork.Persons.Add(person);
            _unitOfWork.Complete();

            _mapper.Map(person, payload);

            return CreatedAtAction(nameof(GetPerson),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        /// <param name="id">Unique person to be updated</param>
        /// <param name="payload">A data transformation object representing the person</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePerson(int id, PersonDto payload)
        {
            var person = new Person();
            try
            {
                person = ValidatePayloadCreatePerson(payload);
            }
            catch (ApplicationException e)
            {
                return StatusCode(new BadRequestResult().StatusCode, e.Message);
            }

            var personInDb = _unitOfWork.Persons.GetEagerLoadedPersonForUpdating(id);
            if (personInDb == null)
                return NotFound();

            _mapper.Map(person, personInDb);
            _mapper.Map(person.Birth, personInDb.Birth);
            _mapper.Map(person.Suffix, personInDb.Suffix);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing person
        /// </summary>
        /// <param name="id">Unique person to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePerson(int id)
        {
            var personInDb = _unitOfWork.Persons.Get(id);

            if (personInDb == null)
                return NotFound();

            _unitOfWork.Persons.Remove(personInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Helper method to validate a <see cref="PersonDto"/> payload that it meets the
        /// requirements of creating and updating a <see cref="Person"/>
        /// </summary>
        /// <param name="payload"><see cref="PersonDto"/> input</param>
        /// <returns><see cref="Person"/> output</returns>
        private Person ValidatePayloadCreatePerson(PersonDto payload)
        {
            var person = _mapper.Map<PersonDto, Person>(payload);

            if (!String.IsNullOrWhiteSpace(person.Suffix.Code))
            {
                try
                {
                    var nameSuffix = _unitOfWork.ReferenceNameSuffixes.Find(p => p.Code == person.Suffix.Code).Single();
                    person.SuffixId = nameSuffix.Id;
                    person.Suffix = nameSuffix;
                }
                catch (InvalidOperationException)
                {
                    throw new ApplicationException($"The suffix \"{person.Suffix.Code}\" is invalid.");
                }
            }

            if (!String.IsNullOrWhiteSpace(person.Birth.StateProvince.Code))
            {
                try
                {
                    var stateProvince = _unitOfWork.ReferenceStateProvinces
                        .Find(p => p.Code == person.Birth.StateProvince.Code).Single();
                    person.Birth.StateProvinceId = stateProvince.Id;
                    person.Birth.StateProvince = stateProvince;
                }
                catch (InvalidOperationException)
                {
                    throw new ApplicationException(
                        $"The birth state/province code \"{person.Birth.StateProvince.Code}\" is invalid.");
                }
            }

            try
            {
                var country = _unitOfWork.ReferenceCountries.Find(p => p.Code == person.Birth.Country.Code).Single();
                person.Birth.CountryId = country.Id;
                person.Birth.Country = country;
            }
            catch (InvalidOperationException)
            {
                throw new ApplicationException($"The birth country code \"{person.Birth.Country.Code}\" is invalid.");
            }

            return person;
        }
    }
}
