using Infra.Services.IService.IUserManagmentServices;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.ValidationServices
{
    public class PermisstionValidationServices : IPermisstionValidationServices
    {
        private readonly IPermisstionServices _permisstionServices;
        public PermisstionValidationServices(IPermisstionServices permisstionServices)
        {
            _permisstionServices = permisstionServices;
        }
        public async Task<bool> IsDescriptionExists(string description, string moduleId)
        => await (await _permisstionServices.
            FindBy(pred => pred.Description.ToLower().Trim() == description.Trim().ToLower() && pred.ModuleId == moduleId)).AnyAsync();

        public async Task<bool> IsDescriptionExists(string id, string description, string moduleId)
        => await (await _permisstionServices.
            FindBy(pred => pred.Id != id && pred.Description.ToLower().Trim() == description.Trim().ToLower() && pred.ModuleId == moduleId)).AnyAsync();

        public async Task<bool> IsNameExists(string name)
         => await (await _permisstionServices.
            FindBy(pred => pred.Name.ToLower().Trim() == name.Trim().ToLower())).AnyAsync();


        public async Task<bool> IsNameExists(string id, string name)
        => await (await _permisstionServices.
            FindBy(pred => pred.Id != id && pred.Name.ToLower().Trim() == name.Trim().ToLower())).AnyAsync();

    }
}
