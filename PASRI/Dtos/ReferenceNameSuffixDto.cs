using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.API.Dtos
{
    public class ReferenceNameSuffixDto : IComparable<ReferenceNameSuffixDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        [Required]
        [StringLength(15)]
        public string Description { get; set; }

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
