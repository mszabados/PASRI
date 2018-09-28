using System;
using System.ComponentModel.DataAnnotations;
using PASRI.API.Persistence.EntityConfigurations;

namespace PASRI.API.Dtos
{
    public class PersonDto
    {
        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PersonConfiguration.FirstNameLength)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PersonConfiguration.LastNameLength)]
        public string LastName { get; set; }

        public string SuffixCode { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        #region For Birth

        public DateTime BirthDate { get; set; }

        [StringLength(BirthConfiguration.BirthCityLength)]
        public string BirthCity { get; set; }
        [StringLength(ReferenceStateProvinceConfiguration.CodeLength)]
        public string BirthStateProvinceCode { get; set; }
        [StringLength(ReferenceCountryConfiguration.CodeLength)]
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
