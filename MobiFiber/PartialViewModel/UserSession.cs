using System;

namespace Micro.Web.PartialViewModel
{
    public class UserSession
    {
        public string SessionId { get; set; }
        public Guid UserId { get; set; }
        public long PartnerId { get; set; }
        public int CityId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public string ImagePath { get; set; }
        public string Token { get; set; }
        public bool IsSupperAdmin { get; set; }
        public int Role { get; set; }
    }
}
