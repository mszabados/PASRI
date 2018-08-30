using System;
using System.ComponentModel.DataAnnotations;

namespace PASRI.Dtos
{
    /// <summary>
    /// Represents the data transfer object for all reference types that inherit from the
    /// <see cref="Core.Domain.ReferenceBase"/>
    /// </summary>
    /// <remarks>
    /// For information why DTOs are being used in this application see the
    /// Patterns of Enterprise Application Architecture from Martin Fowler
    /// https://www.martinfowler.com/eaaCatalog/dataTransferObject.html
    /// </remarks>
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
