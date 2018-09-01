using System.Collections.Generic;

namespace PASRI.API.Core.Domain
{
    public class PersonNameIdentification
    {
        public int Id { get; set; }
        public int PersonIdentificationId { get; set; }
        public virtual PersonIdentification PersonIdentification { get; set; }
        public virtual ICollection<PersonLegalNameIdentification> PersonLegalNameIdentifications { get; set; }
        public int DoDServicePersonDocumentID { get; set; }
    }
}
