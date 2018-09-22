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
    [ApiExplorerSettings(GroupName = "Reference EthnicGroupDemographics")]
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
        /// Get a list of all ethnic groups
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceEthnicGroupDemographicDto>))]
        public IActionResult GetReferenceEthnicGroupDemographics()
        {
            return Ok(_unitOfWork.ReferenceEthnicGroupDemographics.GetAll().Select(_mapper.Map<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>));
        }

        /// <summary>
        /// Get a single ethnic group by its unique ethnic group id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceEthnicGroupDemographicDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceEthnicGroupDemographic(int id)
        {
            var referenceEthnicGroupDemographic = _unitOfWork.ReferenceEthnicGroupDemographics.Get(id);

            if (referenceEthnicGroupDemographic == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>(referenceEthnicGroupDemographic));
        }

        /// <summary>
        /// Create a new ethnic group
        /// </summary>
        /// <param name="payload">A data transformation object representing the ethnic group</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceEthnicGroupDemographicDto))]
        public IActionResult CreateReferenceEthnicGroupDemographic(ReferenceEthnicGroupDemographicDto payload)
        {
            var referenceEthnicGroupDemographic = _mapper.Map<ReferenceEthnicGroupDemographicDto, ReferenceEthnicGroupDemographic>(payload);

            var referenceEthnicGroupDemographicInDb = _unitOfWork.ReferenceEthnicGroupDemographics.Find(p => p.Code == payload.Code);
            if (referenceEthnicGroupDemographicInDb.Count() > 0)
                return new ConflictResult();

            _unitOfWork.ReferenceEthnicGroupDemographics.Add(referenceEthnicGroupDemographic);
            _unitOfWork.Complete();

            payload.Id = referenceEthnicGroupDemographic.Id;

            return CreatedAtAction(nameof(GetReferenceEthnicGroupDemographic),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing ethnic group
        /// </summary>
        /// <param name="id">Unique ethnic group to be updated</param>
        /// <param name="payload">A data transformation object representing the ethnic group</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceEthnicGroupDemographic(int id, ReferenceEthnicGroupDemographicDto payload)
        {
            var referenceEthnicGroupDemographicInDb = _unitOfWork.ReferenceEthnicGroupDemographics.Get(id);
            if (referenceEthnicGroupDemographicInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceEthnicGroupDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing ethnic group
        /// </summary>
        /// <param name="id">Unique ethnic group to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceEthnicGroupDemographic(int id)
        {
            var referenceEthnicGroupDemographicInDb = _unitOfWork.ReferenceEthnicGroupDemographics.Get(id);

            if (referenceEthnicGroupDemographicInDb == null)
                return NotFound();

            _unitOfWork.ReferenceEthnicGroupDemographics.Remove(referenceEthnicGroupDemographicInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}