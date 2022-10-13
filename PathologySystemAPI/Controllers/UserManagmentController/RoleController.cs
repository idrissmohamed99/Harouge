using AutoMapper;
using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IService.IUserManagmentServices;
using Infra.Utili;
using Microsoft.AspNetCore.Mvc;
using HarougeAPI.Filters.UserManagmentFilter.RoleFilter;
using HarougeAPI.Models.UserManagmentModel.RoleModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarougeAPI.Controllers.UserManagmentController
{
    [Route("api/[controller]")]
    public class RoleController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleServices _roleServices;
        private readonly HelperUtili _helper;
        public RoleController(IUnitOfWork unitOfWork, IMapper mapper, IRoleServices roleServices, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleServices = roleServices;
            _helper = helper;
        }

        [HttpPost("InsertRole")]
        [TypeFilter(typeof(InsertRoleFilter))]
        public async Task<ResultOperationDTO<bool>> InsertRole(InsertRoleModel insertModel)
        {
            await _roleServices.InsertRole(_mapper.Map<Roles>(insertModel));
            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية الإضافة بنجاح" });
        }

        [HttpPut("UpdateRole")]
        [TypeFilter(typeof(UpdateRoleFilter))]
        public async Task<ResultOperationDTO<bool>> UpdateRole(UpdateRoleModel updateModel)
        {
            var oldData = await _roleServices.GetById(updateModel.Id);
            await _roleServices.UpdateRole(_mapper.Map(updateModel, oldData));

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية التعديل بنجاح" });
        }

        [HttpGet("GetRoles")]
        public async Task<ResultOperationDTO<PaginationDto<RoleDTO>>>
            GetRoles(string name = "", string moduleId = "", int pageNo = 1, int pageSize = 30)
        {
            var result = await _roleServices.GetRoles(name, moduleId, pageNo, pageSize);
            return ResultOperationDTO<PaginationDto<RoleDTO>>.CreateSuccsessOperation(result);
        }

        [HttpGet("GetActiveRoles")]
        public async Task<ResultOperationDTO<List<ActiveRoleDTO>>> GetActiveRoles(string moduleId, string name = "")
        {
            var result = await _roleServices.GetActiveRoles(name, moduleId);
            return ResultOperationDTO<List<ActiveRoleDTO>>.CreateSuccsessOperation(result);
        }

        [HttpPut("ActivateRole")]
        [TypeFilter(typeof(ActiveRoleFilter))]
        public async Task<ResultOperationDTO<bool>> ActivateRole(string roleId, bool isActive)
        {
            await _roleServices.ActivateRole(roleId, isActive);

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, message: new string[] { "تمت عملية بنجاح" });

        }

        [HttpDelete("DeleteRole")]
        [TypeFilter(typeof(DeleteRoleFilter))]
        public async Task<ResultOperationDTO<bool>> DeleteRole(string roleId)
        {
            await _roleServices.DeleteRole(roleId);

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, message: new string[] { "تمت عملية بنجاح" });
        }






    }
}
