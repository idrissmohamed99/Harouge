using Infra.Domain;
using System.Threading.Tasks;

namespace Infra.ValidationServices.IAuthUserValidation
{
    public interface IUserAuthValidationServices
    {
        Task<Users> CheckUserIsCurrect(string userName, string passwordHash);
        Task<bool> CheckUserIsNotActive(string userId);
        Task<bool> CheckUserIsDelete(string userId);
        Task<bool> CheckUserModule(string userId);
    }
}
