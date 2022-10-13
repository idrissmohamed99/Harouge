using System;
using System.Collections.Generic;

namespace Infra.Domain
{
    public partial class BuildingTypes
    {
        public BuildingTypes()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string BtName { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
