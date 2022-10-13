using System.Threading.Tasks;

namespace Infra.ValidationServices.IUserManagmentServices
{
    public interface IPermisstionValidationServices
    {
        Task<bool> IsDescriptionExists(string description, string moduleId);
        Task<bool> IsDescriptionExists(string id, string description, string moduleId);
        Task<bool> IsNameExists(string name);
        Task<bool> IsNameExists(string id, string name);
    }
}
