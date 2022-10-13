using Infra.DTOs;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarougeAPI.AuthClimas
{
    public class CheckUserIsAdminSytem
    {
        private readonly AuthUser _auth;
        private readonly IOptions<UserSystemDTO> _userSystem;

        public CheckUserIsAdminSytem(AuthUser auth,
            IOptions<UserSystemDTO> userSystem)
        {
            _auth = auth;
            _userSystem = userSystem;

        }

        public async Task<UserAuthDTO> CheckAdminSytem(string userName, string passwordHash)
        {
            var adminSystem = _userSystem.Value;
            UserAuthDTO userAuth = new UserAuthDTO()
            {
                ModuleName = adminSystem.Name,
                Name = adminSystem.Name,
                Permisstions = new List<string>() { adminSystem.Permisstion },
                RoleName = adminSystem.Name,
                Id = adminSystem.Name,
                ModuleId = "",
                RoleId = ""
            };

            if (adminSystem != null)
            {

                if (adminSystem.Name == userName && adminSystem.Password == passwordHash)
                {
                    var createToken = await _auth.SingIn(userAuth);
                    if (string.IsNullOrEmpty(createToken))
                    {
                        userAuth.CheckIsAdminState = CheckIsAdminState.IsErrorCreateToken;
                        return userAuth;
                    }

                    userAuth.AccessToken = createToken;
                    userAuth.CheckIsAdminState = CheckIsAdminState.IsAdmin;

                }
            }
            return userAuth;

        }

    }

}
