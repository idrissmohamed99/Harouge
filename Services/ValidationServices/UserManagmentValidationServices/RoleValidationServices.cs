using Infra.Services.IService.IUserManagmentServices;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ValidationServices
{
    public class RoleValidationServices : IRoleValidationServices
    {
        private readonly IRoleServices _roleServices;
        public RoleValidationServices(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        public async Task<bool> CanDeleteRole(string roleId)
         => await (await _roleServices.FindBy(pred => pred.Id == roleId && pred.Users.Any())).AnyAsync();

        public async Task<bool> CheckRoleName(string roleName, string moduleId)
        => await (await _roleServices.FindBy(pred => pred.ModuleId == moduleId && pred.Name == roleName)).AnyAsync();

        public async Task<bool> CheckRoleName(string roleId, string roleName, string moduleId)
        => await (await _roleServices.FindBy(pred => pred.Id != roleId &&
                    pred.ModuleId == moduleId && pred.Name == roleName)).AnyAsync();

        public async Task<bool> IsDeleteRole(string roleId)
        => await (await _roleServices.FindBy(pred => pred.Id == roleId)).AnyAsync();

    }
}
