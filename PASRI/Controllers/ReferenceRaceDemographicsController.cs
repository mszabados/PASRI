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
    [ApiExplorerSettings(GroupName = "Reference RaceDemographics")]
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
        /// Get a list of all races
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceRaceDemographicDto>))]
        public IActionResult GetReferenceRaceDemographics()
        {
            return Ok(_unitOfWork.ReferenceRaceDemographics.GetAll().Select(_mapper.Map<ReferenceRaceDemographic, ReferenceRaceDemographicDto>));
        }

        /// <summary>
        /// Get a single race by its unique race id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceRaceDemographicDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceRaceDemographic(int id)
        {
            var referenceRaceDemographic = _unitOfWork.ReferenceRaceDemographics.Get(id);

            if (referenceRaceDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceRaceDemographic, ReferenceRaceDemographicDto>(referenceRaceDemographic));
        }

        /// <summary>
        /// Create a new race
        /// </summary>
        /// <param name="payload">A data transformation object representing the race</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceRaceDemographicDto))]
        public IActionResult CreateReferenceRaceDemographic(ReferenceRaceDemographicDto payload)
        {
            var referenceRaceDemographic = _mapper.Map<ReferenceRaceDemographicDto, ReferenceRaceDemographic>(payload);

            var referenceRaceDemographicInDb = _unitOfWork.ReferenceRaceDemographics.Find(p => p.Code == payload.Code);
            if (referenceRaceDemographicInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceRaceDemographics.Add(referenceRaceDemographic);
            _unitOfWork.Complete();

            payload.Id = referenceRaceDemographic.Id;

            return CreatedAtAction(nameof(GetReferenceRaceDemographic),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing race
        /// </summary>
        /// <param name="id">Unique race to be updated</param>
        /// <param name="payload">A data transformation object representing the race</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceRaceDemographic(int id, ReferenceRaceDemographicDto payload)
        {
            var referenceRaceDemographicInDb = _unitOfWork.ReferenceRaceDemographics.Get(id);
            if (referenceRaceDemographicInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceRaceDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing race
        /// </summary>
        /// <param name="id">Unique race to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceRaceDemographic(int id)
        {
            var referenceRaceDemographicInDb = _unitOfWork.ReferenceRaceDemographics.Get(id);

            if (referenceRaceDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceRaceDemographics.Remove(referenceRaceDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}