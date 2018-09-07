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
        /// Get a single country by its unique country code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceCountryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceCountry(string code)
        {
            var referenceCountry = _unitOfWork.ReferenceCountries.Get(code);

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

            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Get(payload.Code);
            if (referenceCountryInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceCountries.Add(referenceCountry);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceCountry),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing country
        /// </summary>
        /// <param name="code">Unique country code to be updated</param>
        /// <param name="payload">A data transformation object representing the country</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceCountry(string code, ReferenceCountryDto payload)
        {
            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Get(code);
            if (referenceCountryInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceCountryInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing country
        /// </summary>
        /// <param name="code">Unique country code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceCountry(string code)
        {
            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Get(code);

            if (referenceCountryInDb == null)
                return NotFound();

            _unitOfWork.ReferenceCountries.Remove(referenceCountryInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}