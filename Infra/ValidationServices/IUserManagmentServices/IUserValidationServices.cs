using System.Threading.Tasks;

namespace Infra.ValidationServices.IUserManagmentServices
{
    public interface IUserValidationServices
    {
        Task<bool> CheckUserName(string name);
        Task<bool> CheckUserName(string id, string name);
        Task<bool> CheckUserEmail(string email);
        Task<bool> CheckUserEmail(string id, string email);
        Task<bool> IsDelete(string id);
    }
}
