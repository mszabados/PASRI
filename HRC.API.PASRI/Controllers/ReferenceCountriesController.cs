using AutoMapper;
using HRC.API.PASRI.Dtos;
using HRC.DB.Master.Core;
using HRC.DB.Master.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HRC.API.PASRI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Reference Countries")]
    public class ReferenceCountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceCountriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all countries
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceCountryDto>))]
        public IActionResult GetReferenceCountries()
        {
            return Ok(_unitOfWork.ReferenceCountries.GetAll().Select(_mapper.Map<ReferenceCountry, ReferenceCountryDto>));
        }

        /// <summary>
        /// Get a single country by its unique country id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceCountryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceCountry(int id)
        {
            var referenceCountry = _unitOfWork.ReferenceCountries.Get(id);

            if (referenceCountry == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceCountry, ReferenceCountryDto>(referenceCountry));
        }

        /// <summary>
        /// Create a new country
        /// </summary>
        /// <param name="payload">A data transformation object representing the country</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceCountryDto))]
        public IActionResult CreateReferenceCountry(ReferenceCountryDto payload)
        {
            var referenceCountry = _mapper.Map<ReferenceCountryDto, ReferenceCountry>(payload);

            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Find(p => p.Code == payload.Code);
            if (referenceCountryInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceCountries.Add(referenceCountry);
            _unitOfWork.Complete();

            payload.Id = referenceCountry.Id;

            return CreatedAtAction(nameof(GetReferenceCountry),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing country
        /// </summary>
        /// <param name="id">Unique country to be updated</param>
        /// <param name="payload">A data transformation object representing the country</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceCountry(int id, ReferenceCountryDto payload)
        {
            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Get(id);
            if (referenceCountryInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceCountryInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing country
        /// </summary>
        /// <param name="id">Unique country to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceCountry(int id)
        {
            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Get(id);

            if (referenceCountryInDb == null)
                return NotFound();

            var referenceStateProvinces = _unitOfWork.ReferenceStateProvinces.Find(sp => sp.CountryId == id);

            _unitOfWork.ReferenceStateProvinces.RemoveRange(referenceStateProvinces);
            _unitOfWork.ReferenceCountries.Remove(referenceCountryInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}