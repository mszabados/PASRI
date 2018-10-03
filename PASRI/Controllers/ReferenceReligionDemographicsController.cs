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
    [ApiExplorerSettings(GroupName = "Reference ReligionDemographics")]
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
        /// Get a list of all religions
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceReligionDemographicDto>))]
        public IActionResult GetReferenceReligionDemographics()
        {
            return Ok(_unitOfWork.ReferenceReligionDemographics.GetAll().Select(_mapper.Map<ReferenceReligionDemographic, ReferenceReligionDemographicDto>));
        }

        /// <summary>
        /// Get a single religion by its unique religion id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceReligionDemographicDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceReligionDemographic(int id)
        {
            var referenceReligionDemographic = _unitOfWork.ReferenceReligionDemographics.Get(id);

            if (referenceReligionDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceReligionDemographic, ReferenceReligionDemographicDto>(referenceReligionDemographic));
        }

        /// <summary>
        /// Create a new religion
        /// </summary>
        /// <param name="payload">A data transformation object representing the religion</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceReligionDemographicDto))]
        public IActionResult CreateReferenceReligionDemographic(ReferenceReligionDemographicDto payload)
        {
            var referenceReligionDemographic = _mapper.Map<ReferenceReligionDemographicDto, ReferenceReligionDemographic>(payload);

            var referenceReligionDemographicInDb = _unitOfWork.ReferenceReligionDemographics.Find(p => p.Code == payload.Code);
            if (referenceReligionDemographicInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceReligionDemographics.Add(referenceReligionDemographic);
            _unitOfWork.Complete();

            payload.Id = referenceReligionDemographic.Id;

            return CreatedAtAction(nameof(GetReferenceReligionDemographic),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing religion
        /// </summary>
        /// <param name="id">Unique religion to be updated</param>
        /// <param name="payload">A data transformation object representing the religion</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceReligionDemographic(int id, ReferenceReligionDemographicDto payload)
        {
            var referenceReligionDemographicInDb = _unitOfWork.ReferenceReligionDemographics.Get(id);
            if (referenceReligionDemographicInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceReligionDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing religion
        /// </summary>
        /// <param name="id">Unique religion to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceReligionDemographic(int id)
        {
            var referenceReligionDemographicInDb = _unitOfWork.ReferenceReligionDemographics.Get(id);

            if (referenceReligionDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceReligionDemographics.Remove(referenceReligionDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}