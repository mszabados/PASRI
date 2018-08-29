using System;

namespace PASRI.Core.Domain
{
    public class ReferenceBase
    {
        public string Code { get; set; }
        public string DisplayText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
