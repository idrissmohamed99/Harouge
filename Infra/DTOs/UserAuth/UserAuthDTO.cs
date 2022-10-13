using System.Collections.Generic;

namespace Infra.DTOs
{
    public class UserAuthDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Permisstions { get; set; }
        public string AccessToken { get; set; }
        public CheckIsAdminState CheckIsAdminState { get; set; } = CheckIsAdminState.IsNotAdmin;


    }
}
