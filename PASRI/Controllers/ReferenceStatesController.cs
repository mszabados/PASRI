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
    [ApiExplorerSettings(GroupName = "Reference States")]
    public class ReferenceStatesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceStatesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all states
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceBaseDto>))]
        public IActionResult GetReferenceState()
        {
            return Ok(_unitOfWork.ReferenceStates.GetAll().Select(_mapper.Map<ReferenceState, ReferenceBaseDto>));
        }

        /// <summary>
        /// Get a single state by its unique state code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(200, Type = typeof(ReferenceBaseDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceState(string code)
        {
            var referenceState = _unitOfWork.ReferenceStates.Get(code);

            if (referenceState == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceState, ReferenceBaseDto>(referenceState));
        }

        /// <summary>
        /// Create a new state
        /// </summary>
        /// <param name="payload">A data transformation object representing the state</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceBaseDto))]
        public IActionResult CreateReferenceState(ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceState = _mapper.Map<ReferenceBaseDto, ReferenceState>(payload);

            var referenceStateInDb = _unitOfWork.ReferenceStates.Get(payload.Code);
            if (referenceStateInDb != null)
                return new ConflictResult();

            _unitOfWork.ReferenceStates.Add(referenceState);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetReferenceState),
                new
                {
                    code = payload.Code
                },
                payload);
        }

        /// <summary>
        /// Update an existing state
        /// </summary>
        /// <param name="code">Unique state code to be updated</param>
        /// <param name="payload">A data transformation object representing the state</param>
        /// <returns></returns>
        [HttpPut("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceState(string code, ReferenceBaseDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var referenceStateInDb = _unitOfWork.ReferenceStates.Get(code);
            if (referenceStateInDb == null)
                return NotFound();

            _mapper.Map<ReferenceBaseDto, ReferenceState>(payload, referenceStateInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing state
        /// </summary>
        /// <param name="code">Unique state code to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceState(string code)
        {
            var referenceStateInDb = _unitOfWork.ReferenceStates.Get(code);

            if (referenceStateInDb == null)
                return NotFound();

            _unitOfWork.ReferenceStates.Remove(referenceStateInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}