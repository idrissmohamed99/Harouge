using Infra.DTOs;
using System.Threading.Tasks;

namespace Infra.Caching
{
    public interface ICachManagment
    {
        Task<bool> IsSetToken();
        Task DeActivateCurrentAsync();
        Task<bool> SaveToken(SetCacheDataTokenDTO token);
        Task DeActivateAsync(string IdTokenRefreshId);
        Task<bool> CheckToken(string IdTokenRefreshId);
    }
}
