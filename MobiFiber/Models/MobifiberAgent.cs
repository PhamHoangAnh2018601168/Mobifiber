using System;
using System.Collections.Generic;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobifiberAgent
    {
        public int AmId { get; set; }
        public string AgentCode { get; set; }
        public string AgentsName { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
