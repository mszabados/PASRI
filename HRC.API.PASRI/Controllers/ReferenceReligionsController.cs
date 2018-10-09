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
    [ApiExplorerSettings(GroupName = "Reference Religions")]
    public class ReferenceReligionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceReligionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all religions
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceReligionDto>))]
        public IActionResult GetReferenceReligions()
        {
            return Ok(_unitOfWork.ReferenceReligions.GetAll().Select(_mapper.Map<ReferenceReligion, ReferenceReligionDto>));
        }

        /// <summary>
        /// Get a single religion by its unique religion id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceReligionDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceReligion(int id)
        {
            var referenceReligion = _unitOfWork.ReferenceReligions.Get(id);

            if (referenceReligion == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceReligion, ReferenceReligionDto>(referenceReligion));
        }

        /// <summary>
        /// Create a new religion
        /// </summary>
        /// <param name="payload">A data transformation object representing the religion</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceReligionDto))]
        public IActionResult CreateReferenceReligion(ReferenceReligionDto payload)
        {
            var referenceReligion = _mapper.Map<ReferenceReligionDto, ReferenceReligion>(payload);

            var referenceReligionInDb = _unitOfWork.ReferenceReligions.Find(p => p.Code == payload.Code);
            if (referenceReligionInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceReligions.Add(referenceReligion);
            _unitOfWork.Complete();

            payload.Id = referenceReligion.Id;

            return CreatedAtAction(nameof(GetReferenceReligion),
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
        public IActionResult UpdateReferenceReligion(int id, ReferenceReligionDto payload)
        {
            var referenceReligionInDb = _unitOfWork.ReferenceReligions.Get(id);
            if (referenceReligionInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceReligionInDb);
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
        public IActionResult DeleteReferenceReligion(int id)
        {
            var referenceReligionInDb = _unitOfWork.ReferenceReligions.Get(id);

            if (referenceReligionInDb == null)
                return NotFound();

            _unitOfWork.ReferenceReligions.Remove(referenceReligionInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}