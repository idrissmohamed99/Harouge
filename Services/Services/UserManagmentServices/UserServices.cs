using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IService.IUserManagmentServices;
using Infra.Utili;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Services.UserManagmentServices
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;
        public UserServices(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }

        public async Task ActivateUser(string userId, bool isActive)
        {
            var result = await _unitOfWork.GetRepository<Users>().GetByID(userId);
            result.IsActive = !isActive;
            await _unitOfWork.GetRepository<Users>().Update(result);
        }

        public async Task DeleteUser(string userId, bool isDelete)
        {
            var result = await _unitOfWork.GetRepository<Users>().GetByID(userId);
            result.IsDelete = !isDelete;
            await _unitOfWork.GetRepository<Users>().Delete(result);
        }

        public async Task<IQueryable<Users>> FindBy(Expression<Func<Users, bool>> predicate)
        => await _unitOfWork.GetRepository<Users>().FindBy(predicate);

        public async Task<Users> GetById(string userId)
        => await (await _unitOfWork.GetRepository<Users>().FindBy(pred => pred.Id == userId)).SingleOrDefaultAsync();


        public async Task<PaginationDto<UserDTO>> GetUsers(string userName, string moduleId, int pageNo, int pageSize, bool isDelete)
        {
            var currentUser = _helper.GetCurrentUser();


            var filterResult = await _unitOfWork.GetRepository<Users>().FindBy(pred =>
                pred.IsDelete == isDelete && pred.Id != currentUser.UserID &&
                (string.IsNullOrWhiteSpace(userName) ? true : pred.Name.Contains(userName)) &&
                (string.IsNullOrEmpty(currentUser.ModuleId) || !string.IsNullOrEmpty(moduleId) ? true : pred.Role.ModuleId == currentUser.ModuleId)
            );

            var totalRecordCount = await filterResult.CountAsync();

            var data = await filterResult.Select(select => new UserDTO
            {
                Id = select.Id,
                Name = select.Name,
                FullName = select.FullName,
                GroupId = select.Role.ModuleId,
                GroupName = select.Role.Module.Name,
                IsActive = select.IsActive,
                CreateAt = select.CreateAt,
                RoleId = select.RoleId,
                RoleName = select.Role.Name,
                Email = select.Email,
                IsDelete = select.IsDelete,

            }).Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationDto<UserDTO>()
            {
                Data = data,
                PageCount = totalRecordCount > 0
              ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
              : 0
            };

        }

        public async Task InsertUser(Users user)
        {
            await _unitOfWork.GetRepository<Users>().Insert(user);
        }

        public async Task ResetPassword(string userId, string newPassword)
        {
            var result = await _unitOfWork.GetRepository<Users>().GetByID(userId);
            result.PasswordHash = _helper.Hash(newPassword);
            await _unitOfWork.GetRepository<Users>().Delete(result);
        }

        public async Task UpdateUser(Users user)
        {
            await _unitOfWork.GetRepository<Users>().Update(user);

        }
    }
}
