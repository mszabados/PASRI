using System;
using System.ComponentModel.DataAnnotations;
using PASRI.API.Persistence.EntityConfigurations;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PASRI.API.Dtos
{
    public class ReferenceEyeColorDto : IComparable<ReferenceEyeColorDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(ReferenceEyeColorConfiguration.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ReferenceEyeColorConfiguration.LongNameLength)]
        public string LongName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceEyeColorDto obj)
        {
            return string.Compare(Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
