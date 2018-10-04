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
    [ApiExplorerSettings(GroupName = "Reference Name Suffixes")]
    public class ReferenceNameSuffixesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceNameSuffixesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all name suffixs
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceNameSuffixDto>))]
        public IActionResult GetReferenceNameSuffixes()
        {
            return Ok(_unitOfWork.ReferenceNameSuffixes.GetAll().Select(_mapper.Map<ReferenceNameSuffix, ReferenceNameSuffixDto>));
        }

        /// <summary>
        /// Get a single name suffix by its unique name suffix id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceNameSuffixDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceNameSuffix(int id)
        {
            var referenceNameSuffix = _unitOfWork.ReferenceNameSuffixes.Get(id);

            if (referenceNameSuffix == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceNameSuffix, ReferenceNameSuffixDto>(referenceNameSuffix));
        }

        /// <summary>
        /// Create a new name suffix
        /// </summary>
        /// <param name="payload">A data transformation object representing the name suffix</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceNameSuffixDto))]
        public IActionResult CreateReferenceNameSuffix(ReferenceNameSuffixDto payload)
        {
            var referenceNameSuffix = _mapper.Map<ReferenceNameSuffixDto, ReferenceNameSuffix>(payload);

            var referenceNameSuffixInDb = _unitOfWork.ReferenceNameSuffixes.Find(p => p.Code == payload.Code);
            if (referenceNameSuffixInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceNameSuffixes.Add(referenceNameSuffix);
            _unitOfWork.Complete();

            payload.Id = referenceNameSuffix.Id;

            return CreatedAtAction(nameof(GetReferenceNameSuffix),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing name suffix
        /// </summary>
        /// <param name="id">Unique name suffix to be updated</param>
        /// <param name="payload">A data transformation object representing the name suffix</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceNameSuffix(int id, ReferenceNameSuffixDto payload)
        {
            var referenceNameSuffixInDb = _unitOfWork.ReferenceNameSuffixes.Get(id);
            if (referenceNameSuffixInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceNameSuffixInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing name suffix
        /// </summary>
        /// <param name="id">Unique name suffix to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceNameSuffix(int id)
        {
            var referenceNameSuffixInDb = _unitOfWork.ReferenceNameSuffixes.Get(id);

            if (referenceNameSuffixInDb == null)
                return NotFound();

            _unitOfWork.ReferenceNameSuffixes.Remove(referenceNameSuffixInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}