using System;

namespace PASRI.Core.Domain
{
    public class PersonLegalNameIdentification
    {
        public int Id { get; set; }
        public int PersonNameIdentificationId { get; set; }
        public PersonNameIdentification PersonNameIdentification { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public string Full { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
