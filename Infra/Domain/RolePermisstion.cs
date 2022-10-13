using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class RolePermisstion
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string PermisstionId { get; set; }

        public virtual Permisstions Permisstion { get; set; }
        public virtual Roles Role { get; set; }
    }
}
