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
    [ApiExplorerSettings(GroupName = "Reference State/Provinces")]
    public class ReferenceStateProvincesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReferenceStateProvincesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all state/provinces
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReferenceStateProvinceDto>))]
        public IActionResult GetReferenceStateProvinces()
        {
            return Ok(_unitOfWork.ReferenceStateProvinces.GetAll().Select(_mapper.Map<ReferenceStateProvince, ReferenceStateProvinceDto>));
        }

        /// <summary>
        /// Get a single state/province by its unique state/province id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReferenceStateProvinceDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReferenceStateProvince(int id)
        {
            var referenceStateProvince = _unitOfWork.ReferenceStateProvinces.Get(id);

            if (referenceStateProvince == null)
                return NotFound();

            return Ok(_mapper.Map<ReferenceStateProvince, ReferenceStateProvinceDto>(referenceStateProvince));
        }

        /// <summary>
        /// Create a new state/province
        /// </summary>
        /// <param name="payload">A data transformation object representing the state/province</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409, Type = typeof(ReferenceStateProvinceDto))]
        public IActionResult CreateReferenceStateProvince(ReferenceStateProvinceDto payload)
        {
            var referenceStateProvince = _mapper.Map<ReferenceStateProvinceDto, ReferenceStateProvince>(payload);

            var referenceStateProvinceInDb = _unitOfWork.ReferenceStateProvinces.Find(p => p.Code == payload.Code);
            if (referenceStateProvinceInDb.Any())
                return new ConflictResult();

            _unitOfWork.ReferenceStateProvinces.Add(referenceStateProvince);
            _unitOfWork.Complete();

            payload.Id = referenceStateProvince.Id;

            return CreatedAtAction(nameof(GetReferenceStateProvince),
                new
                {
                    id = payload.Id
                },
                payload);
        }

        /// <summary>
        /// Update an existing state/province
        /// </summary>
        /// <param name="id">Unique state/province to be updated</param>
        /// <param name="payload">A data transformation object representing the state/province</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReferenceStateProvince(int id, ReferenceStateProvinceDto payload)
        {
            var referenceStateProvinceInDb = _unitOfWork.ReferenceStateProvinces.Get(id);
            if (referenceStateProvinceInDb == null)
                return NotFound();

            _mapper.Map(payload, referenceStateProvinceInDb);
            _unitOfWork.Complete();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing state/province
        /// </summary>
        /// <param name="id">Unique state/province to be deleted</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReferenceStateProvince(int id)
        {
            var referenceStateProvinceInDb = _unitOfWork.ReferenceStateProvinces.Get(id);

            if (referenceStateProvinceInDb == null)
                return NotFound();

            _unitOfWork.ReferenceStateProvinces.Remove(referenceStateProvinceInDb);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}