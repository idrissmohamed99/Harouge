using Infra.DTOs;
using System.Threading.Tasks;

namespace Infra.Services.IAuthUser
{
    public interface IUserAuth
    {
        Task<UserAuthDTO> LoginUser(string userName, string passwprdHash);
        Task<UserAuthDTO> GetInfoUser(string UserID);

    }
}
