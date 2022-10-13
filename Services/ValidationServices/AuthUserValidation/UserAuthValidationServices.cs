using Infra;
using Infra.Domain;
using Infra.Utili;
using Infra.ValidationServices.IAuthUserValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.ValidationServices.AuthUserValidation
{
    public class UserAuthValidationServices : IUserAuthValidationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;

        public UserAuthValidationServices(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }
        public async Task<Users> CheckUserIsCurrect(string userName, string passwordHash)
        {
            var passHash = _helper.Hash(passwordHash);
            var result = await (await _unitOfWork.GetRepository<Users>().
                FindBy(user => user.Name == userName && user.PasswordHash == passHash)).SingleOrDefaultAsync();
            return result;

        }

        public async Task<bool> CheckUserIsDelete(string userId)
        => await (await _unitOfWork.GetRepository<Users>().FindBy(user => user.Id == userId && user.IsDelete == true)).AnyAsync();

        public async Task<bool> CheckUserIsNotActive(string userId)
        => await (await _unitOfWork.GetRepository<Users>().FindBy(user => user.Id == userId && user.IsActive == false)).AnyAsync();

        public async Task<bool> CheckUserModule(string userId)
        => await (await _unitOfWork.GetRepository<Users>().FindBy(pred => pred.Id == userId && pred.Role.Module.IsActive == true)).AnyAsync();

    }
}
