using System;
using System.Collections.Generic;

#nullable disable

namespace MobiFiber.Models
{
    public partial class Account
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public DateTime? Birthday { get; set; }
        public string Skype { get; set; }
        public string Gmail { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public int? StaffTypeId { get; set; }
        public bool? IsHome { get; set; }
    }
}
