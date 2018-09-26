using System;
using System.ComponentModel.DataAnnotations;
using PASRI.API.Persistence.EntityConfigurations;

namespace PASRI.API.Dtos
{
    public class ReferenceNameSuffixDto : IComparable<ReferenceNameSuffixDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(ReferenceNameSuffixConfiguration.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ReferenceNameSuffixConfiguration.LongNameLength)]
        public string LongName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceNameSuffixDto obj)
        {
            return String.Compare(this.Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
