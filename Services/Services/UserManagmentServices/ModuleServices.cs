using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IService.IUserManagmentServices;
using Infra.Utili;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services.UserManagmentServices
{
    public class ModuleServices : IModuleServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;
        public ModuleServices(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }

        public async Task ActivateModule(string moduleId, bool isActive)
        {
            var result = await _unitOfWork.GetRepository<Modules>().GetByID(moduleId);
            result.IsActive = !isActive;
            await _unitOfWork.GetRepository<Modules>().Update(result);
        }

        public async Task<List<ActiveModuleDTO>> GetActiveModules(string name)
        {
            var result = await (await _unitOfWork.GetRepository<Modules>().FindBy(pred =>
               (string.IsNullOrWhiteSpace(name) ? true : pred.Name.StartsWith(name)) &&
               pred.IsActive == true
            )).Select(select => new ActiveModuleDTO
            {
                Id = select.Id,
                Name = select.Name,
            }).Skip(0).Take(50).ToListAsync();

            return result;
        }

        public async Task<PaginationDto<ModuleDTO>> GetModules(string name, int pageNo, int pageSize)
        {
            var filterResult = await _unitOfWork.GetRepository<Modules>().FindBy(pred =>
                    (string.IsNullOrWhiteSpace(name) ? true : pred.Name.Contains(name))
            );

            var totalRecordCount = await filterResult.CountAsync();

            var data = await filterResult.OrderBy(order => order.Id).Select(select => new ModuleDTO
            {
                Id = select.Id,
                IsActive = select.IsActive,
                Name = select.Name

            }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationDto<ModuleDTO>()
            {
                Data = data,
                PageCount = totalRecordCount > 0
                ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                : 0
            };
        }
    }
}
