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
    public class PermisstionServices : IPermisstionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;
        public PermisstionServices(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }

        public async Task ActivatePermisstion(string permisstionId, bool isActive)
        {
            var result = await _unitOfWork.GetRepository<Permisstions>().GetByID(permisstionId);
            result.IsActive = !isActive;
            await _unitOfWork.GetRepository<Permisstions>().Update(result);
        }

        public async Task<IQueryable<Permisstions>> FindBy(Expression<Func<Permisstions, bool>> predicate)
        => await _unitOfWork.GetRepository<Permisstions>().FindBy(predicate);

        public async Task<List<ActivePermisstionDTO>> GetActivePermisstions(string name, string moduleId)
        {
            var result = await (await _unitOfWork.GetRepository<Permisstions>().FindBy(pred =>
                 (string.IsNullOrEmpty(moduleId) ? _helper.GetCurrentUser().ModuleId == moduleId : pred.ModuleId == moduleId) &&
                 (string.IsNullOrWhiteSpace(name) ? true : pred.Name.StartsWith(name))
            )).Select(select => new ActivePermisstionDTO
            {
                Description = select.Description,
                Id = select.Id
            }).Skip(0).Take(50).ToListAsync();

            return result;
        }

        public async Task<Permisstions> GetById(string permisstionId)
        => await _unitOfWork.GetRepository<Permisstions>().GetByID(permisstionId);

        public async Task<PaginationDto<PermisstionDTO>> GetPermisstions(string name, string moduleId, int pageNo, int pageSize)
        {
            var filterResult = await _unitOfWork.GetRepository<Permisstions>().FindBy(pred =>
                (string.IsNullOrEmpty(moduleId) ? true : pred.ModuleId == moduleId) &&
                (string.IsNullOrWhiteSpace(name) ? true : pred.Name.Contains(name))
            );

            var totalRecordCount = await filterResult.CountAsync();

            var data = await filterResult.Select(select => new PermisstionDTO
            {
                Id = select.Id,
                Name = select.Name,
                Description = select.Description,
                ModuleId = select.ModuleId,
                ModuleName = select.Module.Name,
                IsActive = select.IsActive
            }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationDto<PermisstionDTO>()
            {
                Data = data,
                PageCount = totalRecordCount > 0
                ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                : 0
            };
        }

        public async Task InsertPermisstion(Permisstions permisstion)
        {
            await _unitOfWork.GetRepository<Permisstions>().Insert(permisstion);
        }

        public async Task UpdatePermisstion(Permisstions permisstion)
        {
            await _unitOfWork.GetRepository<Permisstions>().Update(permisstion);
        }
    }
}
