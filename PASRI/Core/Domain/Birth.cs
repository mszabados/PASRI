using System;

namespace PASRI.API.Core.Domain
{
    public class Birth
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; private set; }
        public DateTime Date { get; set; }
        public string City { get; set; }

        public int? StateProvinceId { get; set; }
        public virtual ReferenceStateProvince StateProvince { get; private set; }

        public int CountryId { get; set; }
        public virtual ReferenceCountry Country { get; private set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
