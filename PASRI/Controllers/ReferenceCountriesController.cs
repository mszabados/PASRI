using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PASRI.Core;
using PASRI.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using PASRI.Dtos;

namespace PASRI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceCountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceCountriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET /api/ReferenceCountries
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceCountryDto>))]
        public IActionResult GetReferenceCountries()
        {
            return Ok(_unitOfWork.ReferenceCountries.GetAll().Select(_mapper.Map<ReferenceCountry, ReferenceCountryDto>));
        }

        // GET /api/ReferenceCountries/5
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

        // POST /api/ReferenceCountries
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceCountryDto))]
        public IActionResult CreateReferenceCountry(ReferenceCountryDto referenceCountryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceCountry = _mapper.Map<ReferenceCountryDto, ReferenceCountry>(referenceCountryDto);

            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Get(referenceCountryDto.Code);
            if (referenceCountryInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceCountries.Add(referenceCountry);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceCountry),
                new
                {
                    code = referenceCountryDto.Code
                },
                referenceCountryDto);
        }

        // PUT /api/ReferenceCountries/5
        [HttpPut("{code}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceCountry(string code, ReferenceCountryDto referenceCountryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceCountryInDb = _unitOfWork.ReferenceCountries.Get(code);
            if (referenceCountryInDb == null)
                return NotFound();

            _mapper.Map<ReferenceCountryDto, ReferenceCountry>(referenceCountryDto, referenceCountryInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}