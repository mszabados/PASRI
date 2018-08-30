using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.Dtos
{
    public class ReferenceBaseDto
    {
        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string DisplayText { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
