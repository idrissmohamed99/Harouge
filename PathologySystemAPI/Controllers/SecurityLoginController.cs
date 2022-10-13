using Infra.DTOs;
using Infra.Services.IAuthUser;
using Infra.Utili;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HarougeAPI.AuthClimas;
using HarougeAPI.Filters.AuthUserFilter;
using HarougeAPI.Models;
using System.Threading.Tasks;

namespace HarougeAPI.Controllers
{
    [Route(LoginControllerRouter.ControllerNameRoute)]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly IUserAuth _userAuth;


        private readonly AuthUser _auth;
        private readonly HelperUtili _helper;

        public SecurityLoginController(IUserAuth userAuth, AuthUser auth, HelperUtili helper)
        {
            _userAuth = userAuth;
            _auth = auth;
            _helper = helper;
        }

        [AllowAnonymous]
        [HttpPost(LoginControllerRouter.AuthUser)]
        [TypeFilter(typeof(AuthUserFilterAttribute))]
        public async Task<ActionResult<ResultOperationDTO<UserAuthDTO>>> AuthUser([FromBody] UserAuthModel userAuthModel)
        {

            UserAuthDTO result = await _userAuth.LoginUser(userAuthModel.UserName, userAuthModel.PasswordHash);
            if (result == null)
            {
                return ResultOperationDTO<UserAuthDTO>.CreateErrorOperation(messages: new string[] { "تأكد من إسم المستخدم او البريد الالكتروني" });
            }

            string access_Token = await _auth.SingIn(result);

            if (string.IsNullOrEmpty(access_Token))
            {
                return StatusCode(500, "هناك مشكلة في النظام");
            }

            result.AccessToken = access_Token;

            return ResultOperationDTO<UserAuthDTO>.CreateSuccsessOperation(result);

        }

        [HttpGet(LoginControllerRouter.GetUserInfo)]
        public async Task<ActionResult<ResultOperationDTO<UserAuthDTO>>> GetInfoUser()
        {
            UserAuthDTO result = await _userAuth.GetInfoUser(_helper.GetCurrentUser().UserID);
            return ResultOperationDTO<UserAuthDTO>.CreateSuccsessOperation(result);
        }



    }
}