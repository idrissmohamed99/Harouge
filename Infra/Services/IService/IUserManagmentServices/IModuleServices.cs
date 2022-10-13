using Infra.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Services.IService.IUserManagmentServices
{
    public interface IModuleServices
    {
        Task<PaginationDto<ModuleDTO>> GetModules(string name, int pageNo, int pageSize);
        Task<List<ActiveModuleDTO>> GetActiveModules(string name);
        Task ActivateModule(string moduleId, bool isActive);
    }
}
