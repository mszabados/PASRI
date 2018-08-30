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
    [ApiExplorerSettings(GroupName = "Reference Genders")]
    public class ReferenceGenderDemographicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceGenderDemographicsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all gender demographics
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceGenderDemographic()
        {
            return Ok(_unitOfWork.ReferenceGenderDemographics.GetAll().Select(_mapper.Map<ReferenceGenderDemographic, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single gender demographic by its unique gender demographic code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceGenderDemographic(string code)
        {
            var referenceGenderDemographic = _unitOfWork.ReferenceGenderDemographics.Get(code);

            if (referenceGenderDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceGenderDemographic, ReferenceBaseDto>(referenceGenderDemographic));
        }

        /// <summary>
        /// Create a new gender demographic
        /// </summary>
        /// <param name="payload">A data transformation object representing the gender demographic</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceGenderDemographic(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceGenderDemographic = _mapper.Map<ReferenceBaseDto, ReferenceGenderDemographic>(payload);

            var referenceGenderDemographicInDb = _unitOfWork.ReferenceGenderDemographics.Get(payload.Code);
            if (referenceGenderDemographicInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceGenderDemographics.Add(referenceGenderDemographic);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceGenderDemographic),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing gender demographic
        /// </summary>
        /// <param name="code">Unique gender demographic code to be updated</param>
        /// <param name="payload">A data transformation object representing the gender demographic</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceGenderDemographic(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceGenderDemographicInDb = _unitOfWork.ReferenceGenderDemographics.Get(code);
            if (referenceGenderDemographicInDb == null)
                return NotFound();

            _mapper.Map<ReferenceBaseDto, ReferenceGenderDemographic>(payload, referenceGenderDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing gender demographic
        /// </summary>
        /// <param name="code">Unique gender demographic code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceGenderDemographic(string code)
        {
            var referenceGenderDemographicInDb = _unitOfWork.ReferenceGenderDemographics.Get(code);

            if (referenceGenderDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceGenderDemographics.Remove(referenceGenderDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}