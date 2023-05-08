using System;
using System.Collections.Generic;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobifiberConfig
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public Guid? UserCreate { get; set; }
        public DateTime? DateCreate { get; set; }
        public Guid? UserLastUpdate { get; set; }
        public DateTime? DateLastUpdate { get; set; }
    }
}
