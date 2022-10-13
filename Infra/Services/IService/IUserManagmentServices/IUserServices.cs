using Infra.Domain;
using Infra.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Services.IService.IUserManagmentServices
{
    public interface IUserServices
    {
        Task InsertUser(Users user);
        Task UpdateUser(Users user);
        Task ActivateUser(string userId, bool isActive);
        Task DeleteUser(string userId, bool isDelete);
        Task<PaginationDto<UserDTO>> GetUsers(string userName, string moduleId, int pageNo, int pageSize, bool isDelete);
        Task<IQueryable<Users>> FindBy(Expression<Func<Users, bool>> predicate);
        Task<Users> GetById(string userId);
        Task ResetPassword(string userId, string newPassword);
    }
}
