namespace PASRI.Core.Domain
{
    public class PersonIdentification
    {
        public int Id { get; set; } 
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual PersonIdentificationName PersonIdentificationName { get; set; }
    }
}
