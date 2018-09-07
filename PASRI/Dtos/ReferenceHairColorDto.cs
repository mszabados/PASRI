using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.API.Dtos
{
    public class ReferenceHairColorDto : IComparable<ReferenceHairColorDto>
    {
        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string DisplayText { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CompareTo(ReferenceHairColorDto obj)
        {
            return String.Compare(this.Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
