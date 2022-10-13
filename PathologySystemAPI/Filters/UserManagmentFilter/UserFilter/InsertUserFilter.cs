using Infra.DTOs;
using Infra.Utili;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HarougeAPI.Models.UserManagmentModel.UserModel;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.UserFilter
{
    public class InsertUserFilter : ActionFilterAttribute
    {
        private readonly IUserValidationServices _validationServices;
        private readonly HelperUtili _helper;
        public InsertUserFilter(IUserValidationServices validationServices, HelperUtili helper)
        {
            _validationServices = validationServices;
            _helper = helper;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            var param = context.ActionArguments.TryGetValue("insertModel", out var _insertModel);

            if (_insertModel is InsertUserModel insertModel)
            {
                var currentUser = _helper.GetCurrentUser();



                insertModel.PasswordHash = _helper.Hash(insertModel.PasswordHash);


                if (await _validationServices.CheckUserName(insertModel.Name))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { " اسم المستخدم موجود مسبثا" }));
                    return;
                }

                if (await _validationServices.CheckUserEmail(insertModel.Email))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "بريد الإلكتروني موجود مسبثا" }));
                    return;
                }

            }



            await base.OnActionExecutionAsync(context, next);
        }
    }
}
