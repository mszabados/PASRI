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
    [ApiExplorerSettings(GroupName = "Reference Eye Colors")]
    public class ReferenceEyeColorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceEyeColorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all eye colors
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceEyeColor()
        {
            return Ok(_unitOfWork.ReferenceEyeColors.GetAll().Select(_mapper.Map<ReferenceEyeColor, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single eye color by its unique eye color code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceEyeColor(string code)
        {
            var referenceEyeColor = _unitOfWork.ReferenceEyeColors.Get(code);

            if (referenceEyeColor == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceEyeColor, ReferenceBaseDto>(referenceEyeColor));
        }

        /// <summary>
        /// Create a new eye color
        /// </summary>
        /// <param name="payload">A data transformation object representing the eye color</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceEyeColor(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceEyeColor = _mapper.Map<ReferenceBaseDto, ReferenceEyeColor>(payload);

            var referenceEyeColorInDb = _unitOfWork.ReferenceEyeColors.Get(payload.Code);
            if (referenceEyeColorInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceEyeColors.Add(referenceEyeColor);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceEyeColor),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing eye color
        /// </summary>
        /// <param name="code">Unique eye color code to be updated</param>
        /// <param name="payload">A data transformation object representing the eye color</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceEyeColor(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceEyeColorInDb = _unitOfWork.ReferenceEyeColors.Get(code);
            if (referenceEyeColorInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceEyeColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing eye color
        /// </summary>
        /// <param name="code">Unique eye color code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceEyeColor(string code)
        {
            var referenceEyeColorInDb = _unitOfWork.ReferenceEyeColors.Get(code);

            if (referenceEyeColorInDb == null)
                return NotFound();

            _unitOfWork.ReferenceEyeColors.Remove(referenceEyeColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}