﻿using AutoMapper;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceSuffixName()
        {
            return Ok(_unitOfWork.ReferenceSuffixNames.GetAll().Select(_mapper.Map<ReferenceSuffixName, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single suffix name by its unique suffix name code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceSuffixName(string code)
        {
            var referenceSuffixName = _unitOfWork.ReferenceSuffixNames.Get(code);

            if (referenceSuffixName == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceSuffixName, ReferenceBaseDto>(referenceSuffixName));
        }

        /// <summary>
        /// Create a new suffix name
        /// </summary>
        /// <param name="payload">A data transformation object representing the suffix name</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceSuffixName(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceSuffixName = _mapper.Map<ReferenceBaseDto, ReferenceSuffixName>(payload);

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
        public IActionResult UpdateReferenceSuffixName(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceSuffixNameInDb = _unitOfWork.ReferenceSuffixNames.Get(code);
            if (referenceSuffixNameInDb == null)
                return NotFound();

            _mapper.Map<ReferenceBaseDto, ReferenceSuffixName>(payload, referenceSuffixNameInDb);
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