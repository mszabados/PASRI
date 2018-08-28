using System.Collections.Generic;

namespace PASRI.Core.Domain
{
    public class PersonNameIdentification
    {
        public int Id { get; set; }
        public int PersonIdentificationId { get; set; }
        public PersonIdentification PersonIdentification { get; set; }
        public ICollection<PersonLegalNameIdentification> PersonLegalNameIdentifications { get; set; }
        public int DoDServicePersonDocumentID { get; set; }
    }
}
