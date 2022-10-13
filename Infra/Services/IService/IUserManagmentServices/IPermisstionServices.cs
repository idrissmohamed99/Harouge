using Infra.Domain;
using Infra.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Services.IService.IUserManagmentServices
{
    public interface IPermisstionServices
    {
        Task InsertPermisstion(Permisstions permisstion);
        Task UpdatePermisstion(Permisstions permisstion);
        Task<PaginationDto<PermisstionDTO>> GetPermisstions(string name, string moduleId, int pageNo, int pageSize);
        Task<List<ActivePermisstionDTO>> GetActivePermisstions(string name, string moduleId);
        Task ActivatePermisstion(string permisstionId, bool isActive);
        Task<Permisstions> GetById(string permisstionId);
        Task<IQueryable<Permisstions>> FindBy(Expression<Func<Permisstions, bool>> predicate);
    }
}
