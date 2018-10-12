using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace HRC.DB.Master.Core.Domain
{
    public class ReferenceServiceBranchComponent
    {
        public int Id { get; set; }
        public int? ServiceBranchId { get; set; }
        public virtual ReferenceServiceBranch ServiceBranch { get; private set; }
        public string Code { get; set; }
        public string LongName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
