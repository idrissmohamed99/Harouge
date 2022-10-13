using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Permisstions
    {
        public Permisstions()
        {
            RolePermisstion = new HashSet<RolePermisstion>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModuleId { get; set; }
        public bool IsActive { get; set; }

        public virtual Modules Module { get; set; }
        public virtual ICollection<RolePermisstion> RolePermisstion { get; set; }
    }
}
