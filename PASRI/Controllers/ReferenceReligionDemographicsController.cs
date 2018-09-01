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
    [ApiExplorerSettings(GroupName = "Reference Religions")]
    public class ReferenceReligionDemographicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceReligionDemographicsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all religion demographics
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceReligionDemographic()
        {
            return Ok(_unitOfWork.ReferenceReligionDemographics.GetAll().Select(_mapper.Map<ReferenceReligionDemographic, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single religion demographic by its unique religion demographic code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceReligionDemographic(string code)
        {
            var referenceReligionDemographic = _unitOfWork.ReferenceReligionDemographics.Get(code);

            if (referenceReligionDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceReligionDemographic, ReferenceBaseDto>(referenceReligionDemographic));
        }

        /// <summary>
        /// Create a new religion demographic
        /// </summary>
        /// <param name="payload">A data transformation object representing the religion demographic</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceReligionDemographic(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceReligionDemographic = _mapper.Map<ReferenceBaseDto, ReferenceReligionDemographic>(payload);

            var referenceReligionDemographicInDb = _unitOfWork.ReferenceReligionDemographics.Get(payload.Code);
            if (referenceReligionDemographicInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceReligionDemographics.Add(referenceReligionDemographic);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceReligionDemographic),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing religion demographic
        /// </summary>
        /// <param name="code">Unique religion demographic code to be updated</param>
        /// <param name="payload">A data transformation object representing the religion demographic</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceReligionDemographic(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceReligionDemographicInDb = _unitOfWork.ReferenceReligionDemographics.Get(code);
            if (referenceReligionDemographicInDb == null)
                return NotFound();

            _mapper.Map<ReferenceBaseDto, ReferenceReligionDemographic>(payload, referenceReligionDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing religion demographic
        /// </summary>
        /// <param name="code">Unique religion demographic code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceReligionDemographic(string code)
        {
            var referenceReligionDemographicInDb = _unitOfWork.ReferenceReligionDemographics.Get(code);

            if (referenceReligionDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceReligionDemographics.Remove(referenceReligionDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}