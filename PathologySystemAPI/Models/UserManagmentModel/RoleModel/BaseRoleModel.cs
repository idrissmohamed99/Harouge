using System.Collections.Generic;

namespace HarougeAPI.Models.UserManagmentModel.RoleModel
{
    public class BaseRoleModel
    {
        public string Name { get; set; }
        public string ModuleId { get; set; }
        public List<RolePermisstionModel> RolePermisstion { get; set; }
    }
}
