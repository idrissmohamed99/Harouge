using Infra;
using Infra.DTOs;
using Infra.Services.IService.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using HarougeAPI.Filters.UserManagmentFilter.ActiveModuleFilter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarougeAPI.Controllers.UserManagmentController
{
    [Route("api/[controller]")]
    public class ModuleController : BaseController
    {
        private readonly IModuleServices _moduleServices;
        private readonly IUnitOfWork _unitOfWork;
        public ModuleController(IModuleServices moduleServices, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _moduleServices = moduleServices;
        }

        [HttpGet("GetModules")]
        public async Task<ResultOperationDTO<PaginationDto<ModuleDTO>>> GetModules(string name = "", int pageNo = 1, int pageSize = 30)
        {
            var result = await _moduleServices.GetModules(name, pageNo, pageSize);
            return ResultOperationDTO<PaginationDto<ModuleDTO>>.CreateSuccsessOperation(result);
        }

        [HttpGet("GetActiveModules")]
        public async Task<ResultOperationDTO<List<ActiveModuleDTO>>> GetActiveModules(string name = "")
        {
            var result = await _moduleServices.GetActiveModules(name);
            return ResultOperationDTO<List<ActiveModuleDTO>>.CreateSuccsessOperation(result);
        }

        [HttpPut("ActivateModule")]
        [TypeFilter(typeof(ActiveModuleFilter))]
        public async Task<ResultOperationDTO<bool>> ActivateModule(string moduleId, bool isActive)
        {
            await _moduleServices.ActivateModule(moduleId, isActive);

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية بنجاح" });
        }
    }
}
