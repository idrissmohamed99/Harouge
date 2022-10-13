using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Users
    {
        public Users()
        {
            Cases = new HashSet<Cases>();
            Employee = new HashSet<Employee>();
            Printers = new HashSet<Printers>();
            Screens = new HashSet<Screens>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public string CreateUserId { get; set; }
        public string FullName { get; set; }
        public bool? IsSendEmail { get; set; }
        public string RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Cases> Cases { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Printers> Printers { get; set; }
        public virtual ICollection<Screens> Screens { get; set; }
    }
}
