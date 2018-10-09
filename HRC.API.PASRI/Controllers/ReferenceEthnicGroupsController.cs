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
    [ApiExplorerSettings(GroupName = "Reference Ethnic Groups")]
    public class ReferenceEthnicGroupsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceEthnicGroupsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all ethnic groups
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceEthnicGroupDto>))]
        public IActionResult GetReferenceEthnicGroups()
        {
            return Ok(_unitOfWork.ReferenceEthnicGroups.GetAll().Select(_mapper.Map<ReferenceEthnicGroup, ReferenceEthnicGroupDto>));
        }

        /// <summary>
        /// Get a single ethnic group by its unique ethnic group id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceEthnicGroupDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceEthnicGroup(int id)
        {
            var referenceEthnicGroup = _unitOfWork.ReferenceEthnicGroups.Get(id);

            if (referenceEthnicGroup == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceEthnicGroup, ReferenceEthnicGroupDto>(referenceEthnicGroup));
        }

        /// <summary>
        /// Create a new ethnic group
        /// </summary>
        /// <param name="payload">A data transformation object representing the ethnic group</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceEthnicGroupDto))]
        public IActionResult CreateReferenceEthnicGroup(ReferenceEthnicGroupDto payload)
        {
            var referenceEthnicGroup = _mapper.Map<ReferenceEthnicGroupDto, ReferenceEthnicGroup>(payload);

            var referenceEthnicGroupInDb = _unitOfWork.ReferenceEthnicGroups.Find(p => p.Code == payload.Code);
            if (referenceEthnicGroupInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceEthnicGroups.Add(referenceEthnicGroup);
            _unitOfWork.Complete();

            payload.Id = referenceEthnicGroup.Id;

            return CreatedAtAction(nameof(GetReferenceEthnicGroup),
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
        public IActionResult UpdateReferenceEthnicGroup(int id, ReferenceEthnicGroupDto payload)
        {
            var referenceEthnicGroupInDb = _unitOfWork.ReferenceEthnicGroups.Get(id);
            if (referenceEthnicGroupInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceEthnicGroupInDb);
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
        public IActionResult DeleteReferenceEthnicGroup(int id)
        {
            var referenceEthnicGroupInDb = _unitOfWork.ReferenceEthnicGroups.Get(id);

            if (referenceEthnicGroupInDb == null)
                return NotFound();

            _unitOfWork.ReferenceEthnicGroups.Remove(referenceEthnicGroupInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}