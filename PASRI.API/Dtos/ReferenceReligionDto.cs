using System;
using System.ComponentModel.DataAnnotations;
using PASRI.API.Persistence.EntityConfigurations;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PASRI.API.Dtos
{
    public class ReferenceReligionDto : IComparable<ReferenceReligionDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(ReferenceReligionConfiguration.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ReferenceReligionConfiguration.LongNameLength)]
        public string LongName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceReligionDto obj)
        {
            return string.Compare(Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
