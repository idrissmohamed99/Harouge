using Infra.DTOs;
using Infra.ValidationServices.IAuthUserValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HarougeAPI.AuthClimas;
using HarougeAPI.Models;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.AuthUserFilter
{
    public class AuthUserFilterAttribute : ActionFilterAttribute
    {
        private readonly IUserAuthValidationServices _userAuthValidationServices;
        private readonly CheckUserIsAdminSytem _checkUserIsAdminSytem;

        public AuthUserFilterAttribute(IUserAuthValidationServices userAuthValidationServices,
            CheckUserIsAdminSytem checkUserIsAdminSytem)
        {
            _userAuthValidationServices = userAuthValidationServices;
            _checkUserIsAdminSytem = checkUserIsAdminSytem;

        }
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var parm = context.ActionArguments.TryGetValue("userAuthModel", out var _userAuthModel);

            if (parm && _userAuthModel is UserAuthModel userAuthModel)
            {

                var isAdminSystem = await _checkUserIsAdminSytem.CheckAdminSytem(userAuthModel.UserName, userAuthModel.PasswordHash);

                switch (isAdminSystem.CheckIsAdminState)
                {
                    case CheckIsAdminState.IsAdmin:
                        context.Result = new OkObjectResult(ResultOperationDTO<UserAuthDTO>.
                            CreateSuccsessOperation(isAdminSystem));
                        return;

                    case CheckIsAdminState.IsErrorCreateToken:

                        context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                            CreateErrorOperation(messages: new string[] { "هناك مشكلة في إنشاء الـ Token" }));
                        return;
                }
                var result = await _userAuthValidationServices.CheckUserIsCurrect(userAuthModel.UserName, userAuthModel.PasswordHash);

                if (result == null)
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"تأكد من كلمة المرور او اسم المستخدم " }));
                    return;
                }
                if (await _userAuthValidationServices.CheckUserIsDelete(result.Id))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"هذا الحساب تم إلغاءه " }));
                    return;
                }

                if (await _userAuthValidationServices.CheckUserIsNotActive(result.Id))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"هذا الحساب تم إلغاء تفعيله " }));
                    return;
                }

                if (!await _userAuthValidationServices.CheckUserModule(result.Id))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"التبعية التي تتبعها تم إلغاؤها" }));
                    return;
                }


            }



            await base.OnActionExecutionAsync(context, next);
        }

    }
}
