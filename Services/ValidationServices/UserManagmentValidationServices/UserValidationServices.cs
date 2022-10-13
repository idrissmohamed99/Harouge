using Infra.Services.IService.IUserManagmentServices;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.ValidationServices.UserManagmentValidationServices
{
    public class UserValidationServices : IUserValidationServices
    {
        private readonly IUserServices _userServices;
        public UserValidationServices(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public async Task<bool> CheckUserEmail(string email)
        => await (await _userServices.FindBy(pred => pred.Email.Trim().ToLower() == email.ToLower().Trim())).AnyAsync();

        public async Task<bool> CheckUserEmail(string id, string email)
         => await (await _userServices.FindBy(pred => pred.Id != id && pred.Email.Trim().ToLower() == email.ToLower().Trim())).AnyAsync();

        public async Task<bool> CheckUserName(string name)
         => await (await _userServices.FindBy(pred => pred.Name.Trim().ToLower() == name.ToLower().Trim())).AnyAsync();

        public async Task<bool> CheckUserName(string id, string name)
         => await (await _userServices.FindBy(pred => pred.Id != id && pred.Name.Trim().ToLower() == name.ToLower().Trim())).AnyAsync();

        public async Task<bool> IsDelete(string id)
        => await (await _userServices.FindBy(pred => pred.Id == id && pred.IsDelete == true)).AnyAsync();

    }
}
