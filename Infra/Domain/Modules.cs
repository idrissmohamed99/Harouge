using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class Modules
    {
        public Modules()
        {
            Permisstions = new HashSet<Permisstions>();
            Roles = new HashSet<Roles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Permisstions> Permisstions { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}
