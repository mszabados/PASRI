using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PASRI.API.Persistence.EntityConfigurations;

namespace PASRI.API.Dtos
{
    [DataContract]
    public class PersonDto : IComparable<PersonDto>
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        [Required(AllowEmptyStrings = false)]
        [StringLength(PersonConfiguration.FirstNameLength)]
        public string FirstName { get; set; }

        [DataMember]
        [Required(AllowEmptyStrings = false)]
        [StringLength(PersonConfiguration.LastNameLength)]
        public string LastName { get; set; }

        [DataMember]
        public string SuffixCode { get; set; }

        [DataMember]
        public string SuffixLongName { get; set; }

        [DataMember]
        [Required]
        public DateTime EffectiveDate { get; set; }

        #region For Birth

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        [StringLength(BirthConfiguration.BirthCityLength)]
        public string BirthCity { get; set; }

        [DataMember]
        [StringLength(ReferenceStateProvinceConfiguration.CodeLength)]
        public string BirthStateProvinceCode { get; set; }

        [DataMember]
        [StringLength(ReferenceCountryConfiguration.CodeLength)]
        public string BirthCountryCode { get; set; }

        #endregion

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

        public int CompareTo(PersonDto obj)
        {
            int thisId = Id ?? 0;
            return thisId.CompareTo(obj.Id ?? 0);
        }
    }
}
