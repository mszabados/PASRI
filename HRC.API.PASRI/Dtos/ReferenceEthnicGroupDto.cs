using HRC.DB.Master.Persistence.EntityConfigurations;
using System;
using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace HRC.API.PASRI.Dtos
{
    public class ReferenceEthnicGroupDto : IComparable<ReferenceEthnicGroupDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(ReferenceEthnicGroupConfiguration.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ReferenceEthnicGroupConfiguration.LongNameLength)]
        public string LongName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceEthnicGroupDto obj)
        {
            return string.Compare(Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
