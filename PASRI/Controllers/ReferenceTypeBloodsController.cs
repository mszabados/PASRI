using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASRI.Core;
using PASRI.Core.Domain;
using PASRI.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace PASRI.Controllers
{
    [Route("api/ReferenceBloodTypes")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Reference Blood Types")]
    public class ReferenceTypeBloodsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceTypeBloodsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all blood types
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceTypeBlood()
        {
            return Ok(_unitOfWork.ReferenceTypeBloods.GetAll().Select(_mapper.Map<ReferenceTypeBlood, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single blood type by its unique blood type code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceTypeBlood(string code)
        {
            var referenceTypeBlood = _unitOfWork.ReferenceTypeBloods.Get(code);

            if (referenceTypeBlood == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceTypeBlood, ReferenceBaseDto>(referenceTypeBlood));
        }

        /// <summary>
        /// Create a new blood type
        /// </summary>
        /// <param name="payload">A data transformation object representing the blood type</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceTypeBlood(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceTypeBlood = _mapper.Map<ReferenceBaseDto, ReferenceTypeBlood>(payload);

            var referenceTypeBloodInDb = _unitOfWork.ReferenceTypeBloods.Get(payload.Code);
            if (referenceTypeBloodInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceTypeBloods.Add(referenceTypeBlood);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceTypeBlood),
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
        public IActionResult UpdateReferenceTypeBlood(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceTypeBloodInDb = _unitOfWork.ReferenceTypeBloods.Get(code);
            if (referenceTypeBloodInDb == null)
                return NotFound();

            _mapper.Map<ReferenceBaseDto, ReferenceTypeBlood>(payload, referenceTypeBloodInDb);
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
        public IActionResult DeleteReferenceTypeBlood(string code)
        {
            var referenceTypeBloodInDb = _unitOfWork.ReferenceTypeBloods.Get(code);

            if (referenceTypeBloodInDb == null)
                return NotFound();

            _unitOfWork.ReferenceTypeBloods.Remove(referenceTypeBloodInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}