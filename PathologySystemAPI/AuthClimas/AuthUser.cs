using Infra.DTOs;
using Infra.Utili;
using Infra.Utili.ConfigrationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HarougeAPI.AuthClimas
{
    public class AuthUser
    {
        private IOptions<AppSettingsConfig> _settings;

        public AuthUser(IOptions<AppSettingsConfig> settings)
        {
            _settings = settings;
        }

        public async Task<string> SingIn(UserAuthDTO user)
        {
            await Task.FromResult(true);
            DateTime expired = DateTime.Now.AddHours(_settings.Value.AddHourExpired);
            DateTime issuedAt = DateTime.Now;

            var claims = addClaimsIdentity(user, expired);
            return await CreateToken(issuedAt, expired, claims);

        }

        private ClaimsIdentity addClaimsIdentity(UserAuthDTO user, DateTime expired)
        {

            ClaimsIdentity identityClaims = new ClaimsIdentity();
            identityClaims.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
            identityClaims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identityClaims.AddClaim(new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString()));
            identityClaims.AddClaim(new Claim(ClaimTypes.Expired, expired.ToString()));
            identityClaims.AddClaim(new Claim("ModuleName", user.ModuleName));
            identityClaims.AddClaim(new Claim("ModuleId", user.ModuleId));

            foreach (var itemPermissions in user.Permisstions)
            {
                identityClaims.AddClaim(new Claim(ClaimTypes.Role, itemPermissions));
            }



            return identityClaims;

        }

        private async Task<string> CreateToken(DateTime issuedAt, DateTime expired, ClaimsIdentity identityClaims)
        {
            await Task.FromResult(true);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(_settings.Value.Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = _settings.Value.ValidAudience,
                ValidIssuer = _settings.Value.ValidIssuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                LifetimeValidator = HelperUtili.LifetimeValidator,
                IssuerSigningKey = securityKey,
            };
            var createToken =
               (JwtSecurityToken)
                   tokenHandler.CreateJwtSecurityToken(issuer: "", audience: "",
                       subject: identityClaims, notBefore: issuedAt, expires: expired, signingCredentials: signingCredentials);

            return tokenHandler.WriteToken(createToken);
        }

    }
}
