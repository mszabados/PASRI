using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace HRC.DB.Master.Core.Domain
{
    public class ReferencePayPlan
    {
        public int Id { get; set; }
        public int? PersonnelClassId { get; set; }
        public virtual ReferencePersonnelClass PersonnelClass { get; private set; }
        public string Code { get; set; }
        public string LongName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
