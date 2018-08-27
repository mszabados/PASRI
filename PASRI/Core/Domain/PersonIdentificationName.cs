namespace PASRI.Core.Domain
{
    public class PersonIdentificationName
    {
        public int Id { get; set; }
        public int PersonIdentificationId { get; set; }
        public PersonIdentification PersonIdentification { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public string Full { get; set; }
    }
}
