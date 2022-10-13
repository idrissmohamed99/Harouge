using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IService.IUserManagmentServices;
using Infra.Utili;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Services.UserManagmentServices
{
    public class RoleServices : IRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;
        public RoleServices(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }
        public async Task ActivateRole(string roleId, bool isActive)
        {
            var result = await _unitOfWork.GetRepository<Roles>().GetByID(roleId);

            result.IsActive = !isActive;
            await _unitOfWork.GetRepository<Roles>().Update(result);
        }

        public async Task DeleteRole(string roleId)
        {
            var result = await _unitOfWork.GetRepository<Roles>().GetByID(roleId);
            await _unitOfWork.GetRepository<Roles>().Remove(result);
        }

        public async Task<IQueryable<Roles>> FindBy(Expression<Func<Roles, bool>> predicate)
        => await _unitOfWork.GetRepository<Roles>().FindBy(predicate);

        public async Task<List<ActiveRoleDTO>> GetActiveRoles(string name, string moduleId)
        {
            var currentUser = _helper.GetCurrentUser();

            //currentUser.ModuleId = currentUser.ModuleId == ((short)ModuleUserState.Kidney).ToString() ?
            //                        ((short)ModuleUserState.Center).ToString() : currentUser.ModuleId;
            currentUser.ModuleId = currentUser.ModuleId;
            var result = await (await _unitOfWork.GetRepository<Roles>().FindBy(pred =>
                (string.IsNullOrWhiteSpace(name) ? true : pred.Name.StartsWith(name)) &&
                (string.IsNullOrWhiteSpace(moduleId) ? pred.ModuleId == currentUser.ModuleId : pred.ModuleId == moduleId) &&
                pred.IsActive == true &&
                pred.Module.IsActive == true
            )).OrderByDescending(order => order.CreateAt).Select(select => new ActiveRoleDTO
            {
                Id = select.Id,
                Name = select.Name,
            }).Skip(0).Take(50).ToListAsync();

            return result;
        }

        public async Task<Roles> GetById(string roleId)
        => await (await _unitOfWork.GetRepository<Roles>().
            FindBy(pred => pred.Id == roleId)).Include(include => include.RolePermisstion).SingleOrDefaultAsync();

        public async Task<PaginationDto<RoleDTO>> GetRoles(string roleName, string moduleId, int pageNo, int pageSize)
        {

            var currentUser = _helper.GetCurrentUser();
            var filterResult = await _unitOfWork.GetRepository<Roles>().FindBy(pred =>
                (string.IsNullOrWhiteSpace(roleName) ? true : pred.Name.Contains(roleName)) &&
                (string.IsNullOrEmpty(moduleId) ? true : pred.ModuleId == moduleId) &&
                (string.IsNullOrEmpty(currentUser.ModuleId) || !string.IsNullOrEmpty(moduleId) ? true : pred.ModuleId == currentUser.ModuleId)
            );

            var totalRecordCount = await filterResult.CountAsync();

            var data = await filterResult.Select(select => new RoleDTO
            {
                Id = select.Id,
                Name = select.Name,
                ModuleId = select.ModuleId,
                ModuleName = select.Module.Name,
                IsActive = select.IsActive,
                CreateAt = select.CreateAt,
                Permisstions = select.RolePermisstion.Select(select => new ActivePermisstionDTO
                {
                    Description = select.Permisstion.Description,
                    Id = select.PermisstionId
                }).ToList()
            }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationDto<RoleDTO>()
            {
                Data = data,
                PageCount = totalRecordCount > 0
                ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                : 0
            };
        }

        public async Task InsertRole(Roles role)
        {
            await _unitOfWork.GetRepository<Roles>().Insert(role);
        }

        public async Task UpdateRole(Roles role)
        {
            await _unitOfWork.GetRepository<Roles>().Update(role);
        }
    }
}
