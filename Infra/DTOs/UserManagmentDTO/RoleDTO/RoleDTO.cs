using System;
using System.Collections.Generic;

namespace Infra.DTOs
{
    public class RoleDTO : ActiveRoleDTO
    {
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<ActivePermisstionDTO> Permisstions { get; set; }
    }
}
