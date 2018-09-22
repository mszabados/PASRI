using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.API.Dtos
{
    public class ReferenceCountryDto : IComparable<ReferenceCountryDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        [StringLength(44)]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceCountryDto obj)
        {
            return String.Compare(this.Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
