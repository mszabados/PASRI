using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PASRI.API.Core.Domain
{
    public class ReferenceReligion
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string LongName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
