using System;
using System.Collections.Generic;

#nullable disable

namespace MobiFiber.Models
{
    public partial class MobifiberHistory
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Description { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? Action { get; set; }
        public int? IdRefer { get; set; }
        public string FieldChange { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
    }
}
