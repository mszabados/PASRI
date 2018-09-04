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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceBloodType()
        {
            return Ok(_unitOfWork.ReferenceBloodTypes.GetAll().Select(_mapper.Map<ReferenceBloodType, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single blood type by its unique blood type code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceBloodType(string code)
        {
            var referenceBloodType = _unitOfWork.ReferenceBloodTypes.Get(code);

            if (referenceBloodType == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceBloodType, ReferenceBaseDto>(referenceBloodType));
        }

        /// <summary>
        /// Create a new blood type
        /// </summary>
        /// <param name="payload">A data transformation object representing the blood type</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceBloodType(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceBloodType = _mapper.Map<ReferenceBaseDto, ReferenceBloodType>(payload);

            var referenceBloodTypeInDb = _unitOfWork.ReferenceBloodTypes.Get(payload.Code);
            if (referenceBloodTypeInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceBloodTypes.Add(referenceBloodType);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceBloodType),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing blood type
        /// </summary>
        /// <param name="code">Unique blood type code to be updated</param>
        /// <param name="payload">A data transformation object representing the blood type</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceBloodType(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceBloodTypeInDb = _unitOfWork.ReferenceBloodTypes.Get(code);
            if (referenceBloodTypeInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceBloodTypeInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing blood type
        /// </summary>
        /// <param name="code">Unique blood type code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceBloodType(string code)
        {
            var referenceBloodTypeInDb = _unitOfWork.ReferenceBloodTypes.Get(code);

            if (referenceBloodTypeInDb == null)
                return NotFound();

            _unitOfWork.ReferenceBloodTypes.Remove(referenceBloodTypeInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}