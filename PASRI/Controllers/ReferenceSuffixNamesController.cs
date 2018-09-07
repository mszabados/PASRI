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
    [ApiExplorerSettings(GroupName = "Reference Suffix Names")]
    public class ReferenceSuffixNamesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceSuffixNamesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all suffix names
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceSuffixNameDto>))]
        public IActionResult GetReferenceSuffixName()
        {
            return Ok(_unitOfWork.ReferenceSuffixNames.GetAll().Select(_mapper.Map<ReferenceSuffixName, ReferenceSuffixNameDto>));
        }

        /// <summary>
        /// Get a single suffix name by its unique suffix name code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceSuffixNameDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceSuffixName(string code)
        {
            var referenceSuffixName = _unitOfWork.ReferenceSuffixNames.Get(code);

            if (referenceSuffixName == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceSuffixName, ReferenceSuffixNameDto>(referenceSuffixName));
        }

        /// <summary>
        /// Create a new suffix name
        /// </summary>
        /// <param name="payload">A data transformation object representing the suffix name</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceSuffixNameDto))]
        public IActionResult CreateReferenceSuffixName(ReferenceSuffixNameDto payload)
        {
            var referenceSuffixName = _mapper.Map<ReferenceSuffixNameDto, ReferenceSuffixName>(payload);

            var referenceSuffixNameInDb = _unitOfWork.ReferenceSuffixNames.Get(payload.Code);
            if (referenceSuffixNameInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceSuffixNames.Add(referenceSuffixName);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceSuffixName),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing suffix name
        /// </summary>
        /// <param name="code">Unique suffix name code to be updated</param>
        /// <param name="payload">A data transformation object representing the suffix name</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceSuffixName(string code, ReferenceSuffixNameDto payload)
        {
            var referenceSuffixNameInDb = _unitOfWork.ReferenceSuffixNames.Get(code);
            if (referenceSuffixNameInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceSuffixNameInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing suffix name
        /// </summary>
        /// <param name="code">Unique suffix name code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceSuffixName(string code)
        {
            var referenceSuffixNameInDb = _unitOfWork.ReferenceSuffixNames.Get(code);

            if (referenceSuffixNameInDb == null)
                return NotFound();

            _unitOfWork.ReferenceSuffixNames.Remove(referenceSuffixNameInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}