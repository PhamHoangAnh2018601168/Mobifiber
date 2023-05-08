using System;
using System.Collections.Generic;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobifiberDevelopmentUnit
    {
        public int DevelopId { get; set; }
        public string DevelopCode { get; set; }
        public string DevelopName { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
