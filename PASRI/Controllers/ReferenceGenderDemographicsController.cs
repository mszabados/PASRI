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
    [ApiExplorerSettings(GroupName = "Reference GenderDemographics")]
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
        /// Get a list of all genders
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceGenderDemographicDto>))]
        public IActionResult GetReferenceGenderDemographics()
        {
            return Ok(_unitOfWork.ReferenceGenderDemographics.GetAll().Select(_mapper.Map<ReferenceGenderDemographic, ReferenceGenderDemographicDto>));
        }

        /// <summary>
        /// Get a single gender by its unique gender id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceGenderDemographicDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceGenderDemographic(int id)
        {
            var referenceGenderDemographic = _unitOfWork.ReferenceGenderDemographics.Get(id);

            if (referenceGenderDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceGenderDemographic, ReferenceGenderDemographicDto>(referenceGenderDemographic));
        }

        /// <summary>
        /// Create a new gender
        /// </summary>
        /// <param name="payload">A data transformation object representing the gender</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceGenderDemographicDto))]
        public IActionResult CreateReferenceGenderDemographic(ReferenceGenderDemographicDto payload)
        {
            var referenceGenderDemographic = _mapper.Map<ReferenceGenderDemographicDto, ReferenceGenderDemographic>(payload);

            var referenceGenderDemographicInDb = _unitOfWork.ReferenceGenderDemographics.Find(p => p.Code == payload.Code);
            if (referenceGenderDemographicInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceGenderDemographics.Add(referenceGenderDemographic);
            _unitOfWork.Complete();

            payload.Id = referenceGenderDemographic.Id;

            return CreatedAtAction(nameof(GetReferenceGenderDemographic),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing gender
        /// </summary>
        /// <param name="id">Unique gender to be updated</param>
        /// <param name="payload">A data transformation object representing the gender</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceGenderDemographic(int id, ReferenceGenderDemographicDto payload)
        {
            var referenceGenderDemographicInDb = _unitOfWork.ReferenceGenderDemographics.Get(id);
            if (referenceGenderDemographicInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceGenderDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing gender
        /// </summary>
        /// <param name="id">Unique gender to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceGenderDemographic(int id)
        {
            var referenceGenderDemographicInDb = _unitOfWork.ReferenceGenderDemographics.Get(id);

            if (referenceGenderDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceGenderDemographics.Remove(referenceGenderDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}