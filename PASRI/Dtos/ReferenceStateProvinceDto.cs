﻿using System;
using System.ComponentModel.DataAnnotations;
using PASRI.API.Persistence.EntityConfigurations;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PASRI.API.Dtos
{
    public class ReferenceStateProvinceDto : IComparable<ReferenceStateProvinceDto>
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(ReferenceStateProvinceConfiguration.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ReferenceStateProvinceConfiguration.LongNameLength)]
        public string LongName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public int CompareTo(ReferenceStateProvinceDto obj)
        {
            return string.Compare(Code, obj.Code, StringComparison.Ordinal);
        }
    }
}
