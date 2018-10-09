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
    [ApiExplorerSettings(GroupName = "Reference Genders")]
    public class ReferenceGendersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceGendersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all genders
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceGenderDto>))]
        public IActionResult GetReferenceGenders()
        {
            return Ok(_unitOfWork.ReferenceGenders.GetAll().Select(_mapper.Map<ReferenceGender, ReferenceGenderDto>));
        }

        /// <summary>
        /// Get a single gender by its unique gender id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceGenderDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceGender(int id)
        {
            var referenceGender = _unitOfWork.ReferenceGenders.Get(id);

            if (referenceGender == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceGender, ReferenceGenderDto>(referenceGender));
        }

        /// <summary>
        /// Create a new gender
        /// </summary>
        /// <param name="payload">A data transformation object representing the gender</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceGenderDto))]
        public IActionResult CreateReferenceGender(ReferenceGenderDto payload)
        {
            var referenceGender = _mapper.Map<ReferenceGenderDto, ReferenceGender>(payload);

            var referenceGenderInDb = _unitOfWork.ReferenceGenders.Find(p => p.Code == payload.Code);
            if (referenceGenderInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceGenders.Add(referenceGender);
            _unitOfWork.Complete();

            payload.Id = referenceGender.Id;

            return CreatedAtAction(nameof(GetReferenceGender),
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
        public IActionResult UpdateReferenceGender(int id, ReferenceGenderDto payload)
        {
            var referenceGenderInDb = _unitOfWork.ReferenceGenders.Get(id);
            if (referenceGenderInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceGenderInDb);
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
        public IActionResult DeleteReferenceGender(int id)
        {
            var referenceGenderInDb = _unitOfWork.ReferenceGenders.Get(id);

            if (referenceGenderInDb == null)
                return NotFound();

            _unitOfWork.ReferenceGenders.Remove(referenceGenderInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}