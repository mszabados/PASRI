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
        [ApiExplorerSettings(GroupName = "Reference Blood Types")]
        public class ReferenceBloodTypesController : ControllerBase
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

        public ReferenceBloodTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all blood types
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBloodTypeDto>))]
        public IActionResult GetReferenceBloodTypes()
        {
            return Ok(_unitOfWork.ReferenceBloodTypes.GetAll().Select(_mapper.Map<ReferenceBloodType, ReferenceBloodTypeDto>));
        }

        /// <summary>
        /// Get a single blood type by its unique blood type id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBloodTypeDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceBloodType(int id)
        {
            var referenceBloodType = _unitOfWork.ReferenceBloodTypes.Get(id);

            if (referenceBloodType == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceBloodType, ReferenceBloodTypeDto>(referenceBloodType));
        }

        /// <summary>
        /// Create a new blood type
        /// </summary>
        /// <param name="payload">A data transformation object representing the blood type</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBloodTypeDto))]
        public IActionResult CreateReferenceBloodType(ReferenceBloodTypeDto payload)
        {
            var referenceBloodType = _mapper.Map<ReferenceBloodTypeDto, ReferenceBloodType>(payload);

            var referenceBloodTypeInDb = _unitOfWork.ReferenceBloodTypes.Find(p => p.Code == payload.Code);
            if (referenceBloodTypeInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceBloodTypes.Add(referenceBloodType);
            _unitOfWork.Complete();

            payload.Id = referenceBloodType.Id;

            return CreatedAtAction(nameof(GetReferenceBloodType),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing blood type
        /// </summary>
        /// <param name="id">Unique blood type to be updated</param>
        /// <param name="payload">A data transformation object representing the blood type</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceBloodType(int id, ReferenceBloodTypeDto payload)
        {
            var referenceBloodTypeInDb = _unitOfWork.ReferenceBloodTypes.Get(id);
            if (referenceBloodTypeInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceBloodTypeInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing blood type
        /// </summary>
        /// <param name="id">Unique blood type to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceBloodType(int id)
        {
            var referenceBloodTypeInDb = _unitOfWork.ReferenceBloodTypes.Get(id);

            if (referenceBloodTypeInDb == null)
                return NotFound();

            _unitOfWork.ReferenceBloodTypes.Remove(referenceBloodTypeInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}