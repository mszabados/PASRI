using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.API.Dtos
{
    public class ReferenceSuffixNameDto : IComparable<ReferenceSuffixNameDto>
    {
        [Required]
        [StringLength(4)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string DisplayText { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CompareTo(ReferenceSuffixNameDto obj)
        {
            return String.Compare(this.Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
