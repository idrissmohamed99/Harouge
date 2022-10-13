using System.Threading.Tasks;

namespace Infra.ValidationServices.IUserManagmentServices
{
    public interface IRoleValidationServices
    {
        Task<bool> CheckRoleName(string roleName, string moduleId);
        Task<bool> CheckRoleName(string roleId, string roleName, string moduleId);
        Task<bool> IsDeleteRole(string roleId);
        Task<bool> CanDeleteRole(string roleId);
    }
}
