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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceHairColorDto>))]
        public IActionResult GetReferenceHairColors()
        {
            return Ok(_unitOfWork.ReferenceHairColors.GetAll().Select(_mapper.Map<ReferenceHairColor, ReferenceHairColorDto>));
        }

        /// <summary>
        /// Get a single hair color by its unique hair color id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceHairColorDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceHairColor(int id)
        {
            var referenceHairColor = _unitOfWork.ReferenceHairColors.Get(id);

            if (referenceHairColor == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceHairColor, ReferenceHairColorDto>(referenceHairColor));
        }

        /// <summary>
        /// Create a new hair color
        /// </summary>
        /// <param name="payload">A data transformation object representing the hair color</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceHairColorDto))]
        public IActionResult CreateReferenceHairColor(ReferenceHairColorDto payload)
        {
            var referenceHairColor = _mapper.Map<ReferenceHairColorDto, ReferenceHairColor>(payload);

            var referenceHairColorInDb = _unitOfWork.ReferenceHairColors.Find(p => p.Code == payload.Code);
            if (referenceHairColorInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceHairColors.Add(referenceHairColor);
            _unitOfWork.Complete();

            payload.Id = referenceHairColor.Id;

            return CreatedAtAction(nameof(GetReferenceHairColor),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing hair color
        /// </summary>
        /// <param name="id">Unique hair color to be updated</param>
        /// <param name="payload">A data transformation object representing the hair color</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceHairColor(int id, ReferenceHairColorDto payload)
        {
            var referenceHairColorInDb = _unitOfWork.ReferenceHairColors.Get(id);
            if (referenceHairColorInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceHairColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing hair color
        /// </summary>
        /// <param name="id">Unique hair color to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceHairColor(int id)
        {
            var referenceHairColorInDb = _unitOfWork.ReferenceHairColors.Get(id);

            if (referenceHairColorInDb == null)
                return NotFound();

            _unitOfWork.ReferenceHairColors.Remove(referenceHairColorInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}