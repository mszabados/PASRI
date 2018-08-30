using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASRI.Core;
using PASRI.Core.Domain;
using PASRI.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace PASRI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Reference Races")]
    public class ReferenceRaceDemographicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceRaceDemographicsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all race demographics
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceRaceDemographic()
        {
            return Ok(_unitOfWork.ReferenceRaceDemographics.GetAll().Select(_mapper.Map<ReferenceRaceDemographic, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single race demographic by its unique race demographic code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceRaceDemographic(string code)
        {
            var referenceRaceDemographic = _unitOfWork.ReferenceRaceDemographics.Get(code);

            if (referenceRaceDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceRaceDemographic, ReferenceBaseDto>(referenceRaceDemographic));
        }

        /// <summary>
        /// Create a new race demographic
        /// </summary>
        /// <param name="payload">A data transformation object representing the race demographic</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceRaceDemographic(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceRaceDemographic = _mapper.Map<ReferenceBaseDto, ReferenceRaceDemographic>(payload);

            var referenceRaceDemographicInDb = _unitOfWork.ReferenceRaceDemographics.Get(payload.Code);
            if (referenceRaceDemographicInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceRaceDemographics.Add(referenceRaceDemographic);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceRaceDemographic),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing race demographic
        /// </summary>
        /// <param name="code">Unique race demographic code to be updated</param>
        /// <param name="payload">A data transformation object representing the race demographic</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceRaceDemographic(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceRaceDemographicInDb = _unitOfWork.ReferenceRaceDemographics.Get(code);
            if (referenceRaceDemographicInDb == null)
                return NotFound();

            _mapper.Map<ReferenceBaseDto, ReferenceRaceDemographic>(payload, referenceRaceDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing race demographic
        /// </summary>
        /// <param name="code">Unique race demographic code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceRaceDemographic(string code)
        {
            var referenceRaceDemographicInDb = _unitOfWork.ReferenceRaceDemographics.Get(code);

            if (referenceRaceDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceRaceDemographics.Remove(referenceRaceDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}