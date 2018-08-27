using System.Collections.Generic;

namespace PASRI.Core.Domain
{
    public class Person
    {
        public int Id { get; set; }

        public virtual ICollection<PersonIdentification> PersonIdentifications { get; set; }
    }
}
