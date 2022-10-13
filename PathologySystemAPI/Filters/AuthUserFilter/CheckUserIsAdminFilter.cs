using Infra.DTOs;
using Infra.Utili;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using HarougeAPI.AuthClimas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.AuthUserFilter
{
    public class CheckUserIsAdminFilter : ActionFilterAttribute
    {
        private readonly CheckUserIsAdminSytem _checkUserIsAdminSytem;
        private readonly HelperUtili _helper;
        private readonly IOptions<UserSystemDTO> _userSystem;

        public CheckUserIsAdminFilter(CheckUserIsAdminSytem checkUserIsAdminSytem, HelperUtili helper,
            IOptions<UserSystemDTO> userSystem)
        {
            _checkUserIsAdminSytem = checkUserIsAdminSytem;
            _helper = helper;
            _userSystem = userSystem;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var currentUser = _helper.GetCurrentUser();
            if (currentUser != null)
            {
                var adminSystem = _userSystem.Value;

                if (currentUser.UserID == adminSystem.Id)
                {
                    var userAuth = new UserAuthDTO()
                    {
                        ModuleName = adminSystem.Name,
                        Name = adminSystem.Name,
                        Permisstions = new List<string>() { adminSystem.Permisstion },
                        RoleName = adminSystem.Name,
                        Id = adminSystem.Name,

                        ModuleId = "",
                        RoleId = "",
                    };
                    context.Result = new OkObjectResult(
                        ResultOperationDTO<UserAuthDTO>.CreateSuccsessOperation(userAuth));
                    return;
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
