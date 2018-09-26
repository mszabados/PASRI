using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASRI.API.Core.Domain
{
    public class Birth
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }

        public int StateProvinceId { get; set; }
        public ReferenceStateProvince StateProvince { get; set; }

        public int CountryId { get; set; }
        public ReferenceCountry Country { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
