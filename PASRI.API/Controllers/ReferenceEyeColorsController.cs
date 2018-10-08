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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceEyeColorDto>))]
        public IActionResult GetReferenceEyeColors()
        {
            return Ok(_unitOfWork.ReferenceEyeColors.GetAll().Select(_mapper.Map<ReferenceEyeColor, ReferenceEyeColorDto>));
        }

        /// <summary>
        /// Get a single eye color by its unique eye color id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceEyeColorDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceEyeColor(int id)
        {
            var referenceEyeColor = _unitOfWork.ReferenceEyeColors.Get(id);

            if (referenceEyeColor == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceEyeColor, ReferenceEyeColorDto>(referenceEyeColor));
        }

        /// <summary>
        /// Create a new eye color
        /// </summary>
        /// <param name="payload">A data transformation object representing the eye color</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceEyeColorDto))]
        public IActionResult CreateReferenceEyeColor(ReferenceEyeColorDto payload)
        {
            var referenceEyeColor = _mapper.Map<ReferenceEyeColorDto, ReferenceEyeColor>(payload);

            var referenceEyeColorInDb = _unitOfWork.ReferenceEyeColors.Find(p => p.Code == payload.Code);
            if (referenceEyeColorInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceEyeColors.Add(referenceEyeColor);
            _unitOfWork.Complete();

            payload.Id = referenceEyeColor.Id;

            return CreatedAtAction(nameof(GetReferenceEyeColor),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing eye color
        /// </summary>
        /// <param name="id">Unique eye color to be updated</param>
        /// <param name="payload">A data transformation object representing the eye color</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceEyeColor(int id, ReferenceEyeColorDto payload)
        {
            var referenceEyeColorInDb = _unitOfWork.ReferenceEyeColors.Get(id);
            if (referenceEyeColorInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceEyeColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing eye color
        /// </summary>
        /// <param name="id">Unique eye color to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceEyeColor(int id)
        {
            var referenceEyeColorInDb = _unitOfWork.ReferenceEyeColors.Get(id);

            if (referenceEyeColorInDb == null)
                return NotFound();

            _unitOfWork.ReferenceEyeColors.Remove(referenceEyeColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}