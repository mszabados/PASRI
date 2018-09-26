using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.API.Dtos
{
    public class PersonDto
    {
        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(255)]
        public string LastName { get; set; }

        public string SuffixCode { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        #region For Birth

        public DateTime BirthDate { get; set; }
        public string BirthCity { get; set; }
        [StringLength(2)]
        public string BirthStateProvinceCode { get; set; }
        [StringLength(2)]
        public string BirthCountryCode { get; set; }

        #endregion

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(PersonDto obj)
        {
            int thisId = Id ?? 0;
            return thisId.CompareTo(obj.Id ?? 0);
        }
    }
}
