using System;

namespace PASRI.API.Core.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SuffixId { get; set; }
        public ReferenceNameSuffix Suffix { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        #region Relationships

        public Birth Birth { get; set; }

        #endregion
    }
}
