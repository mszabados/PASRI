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
    [ApiExplorerSettings(GroupName = "Reference Races")]
    public class ReferenceRacesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceRacesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all races
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceRaceDto>))]
        public IActionResult GetReferenceRaces()
        {
            return Ok(_unitOfWork.ReferenceRaces.GetAll().Select(_mapper.Map<ReferenceRace, ReferenceRaceDto>));
        }

        /// <summary>
        /// Get a single race by its unique race id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceRaceDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceRace(int id)
        {
            var referenceRace = _unitOfWork.ReferenceRaces.Get(id);

            if (referenceRace == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceRace, ReferenceRaceDto>(referenceRace));
        }

        /// <summary>
        /// Create a new race
        /// </summary>
        /// <param name="payload">A data transformation object representing the race</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceRaceDto))]
        public IActionResult CreateReferenceRace(ReferenceRaceDto payload)
        {
            var referenceRace = _mapper.Map<ReferenceRaceDto, ReferenceRace>(payload);

            var referenceRaceInDb = _unitOfWork.ReferenceRaces.Find(p => p.Code == payload.Code);
            if (referenceRaceInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceRaces.Add(referenceRace);
            _unitOfWork.Complete();

            payload.Id = referenceRace.Id;

            return CreatedAtAction(nameof(GetReferenceRace),
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
        public IActionResult UpdateReferenceRace(int id, ReferenceRaceDto payload)
        {
            var referenceRaceInDb = _unitOfWork.ReferenceRaces.Get(id);
            if (referenceRaceInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceRaceInDb);
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
        public IActionResult DeleteReferenceRace(int id)
        {
            var referenceRaceInDb = _unitOfWork.ReferenceRaces.Get(id);

            if (referenceRaceInDb == null)
                return NotFound();

            _unitOfWork.ReferenceRaces.Remove(referenceRaceInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}