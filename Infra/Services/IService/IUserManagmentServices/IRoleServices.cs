using Infra.Domain;
using Infra.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Services.IService.IUserManagmentServices
{
    public interface IRoleServices
    {
        Task InsertRole(Roles role);
        Task UpdateRole(Roles role);
        Task DeleteRole(string roleId);
        Task ActivateRole(string roleId, bool isActive);
        Task<PaginationDto<RoleDTO>> GetRoles(string roleName, string moduleId, int pageNo, int pageSize);
        Task<List<ActiveRoleDTO>> GetActiveRoles(string name, string moduleId);
        Task<IQueryable<Roles>> FindBy(Expression<Func<Roles, bool>> predicate);
        Task<Roles> GetById(string roleId);

    }
}
