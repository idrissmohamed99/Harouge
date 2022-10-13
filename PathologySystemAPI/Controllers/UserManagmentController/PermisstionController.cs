using AutoMapper;
using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IService.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using HarougeAPI.Filters.UserManagmentFilter.PermisstionFilter;
using HarougeAPI.Models.UserManagmentModel.PermisstionModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarougeAPI.Controllers.UserManagmentController
{
    [Route("api/[controller]")]
    public class PermisstionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermisstionServices _permisstionServices;
        public PermisstionController(IPermisstionServices permisstionServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _permisstionServices = permisstionServices;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("InsertPermisstion")]
        [TypeFilter(typeof(InsertPermisstionFilter))]
        public async Task<ResultOperationDTO<bool>> InsertPermisstion(InsertPermisstionModel insertModel)
        {
            await _permisstionServices.InsertPermisstion(_mapper.Map<Permisstions>(insertModel));
            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية الإضافة بنجاح" });
        }

        [HttpPut("UpdatePermisstion")]
        [TypeFilter(typeof(UpdatePermisstionFilter))]
        public async Task<ResultOperationDTO<bool>> UpdatePermisstion(UpdatePermisstionModel updateModel)
        {
            var oldData = await _permisstionServices.GetById(updateModel.Id);
            await _permisstionServices.UpdatePermisstion(_mapper.Map(updateModel, oldData));

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية التعديل بنجاح" });
        }

        [HttpGet("GetPermisstions")]
        public async Task<ResultOperationDTO<PaginationDto<PermisstionDTO>>> GetPermisstions(string name = "", int pageNo = 1, int pageSize = 30,
            string moduleId = "")
        {
            var result = await _permisstionServices.GetPermisstions(name, moduleId, pageNo, pageSize);
            return ResultOperationDTO<PaginationDto<PermisstionDTO>>.CreateSuccsessOperation(result);
        }

        [HttpGet("GetActivePermisstions")]
        public async Task<ResultOperationDTO<List<ActivePermisstionDTO>>> GetActivePermisstions(string moduleId, string name = "")
        {
            var result = await _permisstionServices.GetActivePermisstions(name, moduleId);
            return ResultOperationDTO<List<ActivePermisstionDTO>>.CreateSuccsessOperation(result);
        }

        [HttpPut("ActivatePermisstion")]
        [TypeFilter(typeof(ActivePermisstionFilter))]
        public async Task<ResultOperationDTO<bool>> ActivatePermisstion(string permisstionId, bool isActive)
        {
            await _permisstionServices.ActivatePermisstion(permisstionId, isActive);

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية بنجاح" });
        }
    }
}
