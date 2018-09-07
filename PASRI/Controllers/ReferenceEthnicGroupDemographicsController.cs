﻿using System.Collections.Generic;
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
    [ApiExplorerSettings(GroupName = "Reference Ethnic Groups")]
    public class ReferenceEthnicGroupDemographicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceEthnicGroupDemographicsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all ethnic group demographics
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceEthnicGroupDemographicDto>))]
        public IActionResult GetReferenceEthnicGroupDemographics()
        {
            return Ok(_unitOfWork.ReferenceEthnicGroupDemographics.GetAll().Select(_mapper.Map<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>));
        }

        /// <summary>
        /// Get a single ethnic group demographic by its unique ethnic group demographic code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceEthnicGroupDemographicDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceEthnicGroupDemographic(string code)
        {
            var referenceEthnicGroupDemographic = _unitOfWork.ReferenceEthnicGroupDemographics.Get(code);

            if (referenceEthnicGroupDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>(referenceEthnicGroupDemographic));
        }

        /// <summary>
        /// Create a new ethnic group demographic
        /// </summary>
        /// <param name="payload">A data transformation object representing the ethnic group demographic</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceEthnicGroupDemographicDto))]
        public IActionResult CreateReferenceEthnicGroupDemographic(ReferenceEthnicGroupDemographicDto payload)
        {
            var referenceEthnicGroupDemographic = _mapper.Map<ReferenceEthnicGroupDemographicDto, ReferenceEthnicGroupDemographic>(payload);

            var referenceEthnicGroupDemographicInDb = _unitOfWork.ReferenceEthnicGroupDemographics.Get(payload.Code);
            if (referenceEthnicGroupDemographicInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceEthnicGroupDemographics.Add(referenceEthnicGroupDemographic);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceEthnicGroupDemographic),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing ethnic group demographic
        /// </summary>
        /// <param name="code">Unique ethnic group demographic code to be updated</param>
        /// <param name="payload">A data transformation object representing the ethnic group demographic</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceEthnicGroupDemographic(string code, ReferenceEthnicGroupDemographicDto payload)
        {
            var referenceEthnicGroupDemographicInDb = _unitOfWork.ReferenceEthnicGroupDemographics.Get(code);
            if (referenceEthnicGroupDemographicInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceEthnicGroupDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing ethnic group demographic
        /// </summary>
        /// <param name="code">Unique ethnic group demographic code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceEthnicGroupDemographic(string code)
        {
            var referenceEthnicGroupDemographicInDb = _unitOfWork.ReferenceEthnicGroupDemographics.Get(code);

            if (referenceEthnicGroupDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceEthnicGroupDemographics.Remove(referenceEthnicGroupDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}