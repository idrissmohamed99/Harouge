using Infra.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infra.Utili
{
    public class HelperUtili
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public HelperUtili(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserClimesDTO GetCurrentUser()
        {
            try
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
                if (claims.Count == 0)
                {
                    return null;
                }

                return new UserClimesDTO
                {
                    UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value,
                    UserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value,
                    ModuleId = _httpContextAccessor.HttpContext.User.FindFirst("ModuleId").Value,
                    ModuleName = _httpContextAccessor.HttpContext.User.FindFirst("ModuleName").Value
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                var dateTimeExpires = expires.Value.ToLocalTime();
                if (DateTime.Now < dateTimeExpires)
                {
                    return true;
                }
            }
            return false;
        }

        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyHash(string inputPassword, string Passwordhash)
        {
            var hashOfInput = this.Hash(inputPassword);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, Passwordhash) == 0;
        }





    }
}
