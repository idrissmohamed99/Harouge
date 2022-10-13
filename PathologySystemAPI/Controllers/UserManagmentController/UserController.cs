using AutoMapper;
using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IService.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using HarougeAPI.Filters.UserManagmentFilter.UserFilter;
using HarougeAPI.Models.UserManagmentModel.UserModel;
using System.Threading.Tasks;

namespace HarougeAPI.Controllers.UserManagmentController
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUserServices userServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userServices = userServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetUsers")]
        public async Task<ResultOperationDTO<PaginationDto<UserDTO>>>
            GetUsers(string name = "", string moduleId = "", int pageNo = 1, int pageSize = 30, bool isDelete = false)
        {
            var result = await _userServices.GetUsers(name, moduleId, pageNo, pageSize, isDelete);
            return ResultOperationDTO<PaginationDto<UserDTO>>.CreateSuccsessOperation(result);
        }

        [HttpPost("InsertUser")]
        [TypeFilter(typeof(InsertUserFilter))]
        public async Task<ResultOperationDTO<bool>> InsertUser(InsertUserModel insertModel)
        {

            await _userServices.InsertUser(_mapper.Map<Users>(insertModel));

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية الإضافة بنجاح" });
        }


        [HttpPut("UpdateUser")]
        [TypeFilter(typeof(UpdateUserFilter))]
        public async Task<ResultOperationDTO<bool>> UpdateUser(UpdateUserModel updateModel)
        {
            var oldData = await _userServices.GetById(updateModel.Id);
            await _userServices.UpdateUser(_mapper.Map(updateModel, oldData));

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية التعديل بنجاح" });
        }

        [HttpPut("ActivateUser")]
        [TypeFilter(typeof(ActiveUserFilter))]
        public async Task<ResultOperationDTO<bool>> ActivateUser(string userId, bool isActive)
        {
            await _userServices.ActivateUser(userId, isActive);

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية بنجاح" });
        }

        [HttpDelete("DeleteUser")]
        [TypeFilter(typeof(DeleteUserFilter))]
        public async Task<ResultOperationDTO<bool>> DeleteUser(string userId, bool isDelete)
        {
            await _userServices.DeleteUser(userId, isDelete);

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية بنجاح" });
        }

        [HttpPut("ResetPassword")]
        [TypeFilter(typeof(ResetPasswordFilter))]
        public async Task<ResultOperationDTO<bool>> ResetPassword(string userId, string newPassword)
        {
            await _userServices.ResetPassword(userId, newPassword);

            await _unitOfWork.SaveChangeAsync();
            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تم عملية بنجاح" });
        }

    }
}
