using HRC.DB.Master.Persistence.EntityConfigurations;
using System;
using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace HRC.API.PASRI.Dtos
{
    public class ReferenceGenderDto : IComparable<ReferenceGenderDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(ReferenceGenderConfiguration.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ReferenceGenderConfiguration.LongNameLength)]
        public string LongName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceGenderDto obj)
        {
            return string.Compare(Code, obj.Code, StringComparison.Ordinal);
        }
    }
}