using System;
using System.Collections.Generic;

#nullable disable

namespace MobiFiber.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int? ModuleId { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }
        public bool Delete { get; set; }
        public bool Import { get; set; }
        public bool Export { get; set; }
        public bool Upload { get; set; }
        public bool Publish { get; set; }
        public bool Report { get; set; }
    }
}
