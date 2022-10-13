using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Roles
    {
        public Roles()
        {
            RolePermisstion = new HashSet<RolePermisstion>();
            Users = new HashSet<Users>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public string CreateUserId { get; set; }
        public string ModuleId { get; set; }

        public virtual Modules Module { get; set; }
        public virtual ICollection<RolePermisstion> RolePermisstion { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
