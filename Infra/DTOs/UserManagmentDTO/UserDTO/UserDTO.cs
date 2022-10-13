using System;

namespace Infra.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }

    }
}
