﻿using System;

namespace PASRI.API.Core.Domain
{
    public class ReferenceHairColor
    {
        public string Code { get; set; }
        public string DisplayText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
