using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IAuthUser;
using Infra.Utili;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services.AuthUser
{
    public class UserAuth : IUserAuth
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;
        public UserAuth(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }

        public async Task<UserAuthDTO> GetInfoUser(string UserID)
        {
            var result = await (await _unitOfWork.GetRepository<Users>().FindBy(pred =>
            pred.Id == UserID))
            .Select(select => new UserAuthDTO
            {
                ModuleName = select.Role.Module.Name,
                RoleId = select.RoleId,
                RoleName = select.Role.Name,

                Permisstions = select.Role.RolePermisstion.
                    Where(pred => pred.Role.IsActive == true &&
                    pred.Role.Module.IsActive == true && pred.Permisstion.IsActive == true).Select(select => select.Permisstion.Name).ToList(),
                Name = select.Name,
                ModuleId = select.Role.ModuleId,

            }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<UserAuthDTO> LoginUser(string userName, string passwprdHash)
        {
            var passHash = _helper.Hash(passwprdHash);
            var result = await (await _unitOfWork.GetRepository<Users>().
                FindBy(user => user.Name == userName && user.PasswordHash == passHash)).
                Select(select => new UserAuthDTO
                {

                    Id = select.Id,
                    ModuleId = select.Role.ModuleId,
                    ModuleName = select.Role.Module.Name,
                    Name = select.Name,
                    RoleId = select.RoleId,
                    RoleName = select.Role.Name,
                    Permisstions = select.Role.RolePermisstion.
                    Where(pred => pred.Role.IsActive == true &&
                    pred.Role.Module.IsActive == true &&
                    pred.Permisstion.IsActive == true).Select(select => select.Permisstion.Name).ToList(),
                }).SingleOrDefaultAsync();

            return result;
        }


    }
}
