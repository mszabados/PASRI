using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PASRI.Core;
using PASRI.Core.Domain;
using PASRI.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace PASRI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Reference Hair Colors")]
    public class ReferenceHairColorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceHairColorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all hair colors
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceHairColor()
        {
            return Ok(_unitOfWork.ReferenceHairColors.GetAll().Select(_mapper.Map<ReferenceHairColor, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single hair color by its unique hair color code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceHairColor(string code)
        {
            var referenceHairColor = _unitOfWork.ReferenceHairColors.Get(code);

            if (referenceHairColor == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceHairColor, ReferenceBaseDto>(referenceHairColor));
        }

        /// <summary>
        /// Create a new hair color
        /// </summary>
        /// <param name="payload">A data transformation object representing the hair color</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceHairColor(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceHairColor = _mapper.Map<ReferenceBaseDto, ReferenceHairColor>(payload);

            var referenceHairColorInDb = _unitOfWork.ReferenceHairColors.Get(payload.Code);
            if (referenceHairColorInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceHairColors.Add(referenceHairColor);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceHairColor),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing hair color
        /// </summary>
        /// <param name="code">Unique hair color code to be updated</param>
        /// <param name="payload">A data transformation object representing the hair color</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceHairColor(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceHairColorInDb = _unitOfWork.ReferenceHairColors.Get(code);
            if (referenceHairColorInDb == null)
                return NotFound();

            _mapper.Map<ReferenceBaseDto, ReferenceHairColor>(payload, referenceHairColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing hair color
        /// </summary>
        /// <param name="code">Unique hair color code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceHairColor(string code)
        {
            var referenceHairColorInDb = _unitOfWork.ReferenceHairColors.Get(code);

            if (referenceHairColorInDb == null)
                return NotFound();

            _unitOfWork.ReferenceHairColors.Remove(referenceHairColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}