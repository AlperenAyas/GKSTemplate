using GKS.Application.Abstrations.Services;
using GKS.Application.Features.CQRS.Requests.QueryRequests;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(LoginQueryRequest request)
        {
            // TODO: GetSection() iç içe dataya bak.[TokenIssuer]
            var securityToken = new JwtSecurityToken(
            //    issuer: "http://YSKALPHA",
            //    audience: _options.Value.Audience,
                claims: GetClaims(request.Username),
                expires: DateTime.Now.AddDays(5),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("BanaYalanSoylediler?EVETSOYLEDILER")),
                    SecurityAlgorithms.HmacSha256)
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(securityToken);
        }

        private List<Claim> GetClaims(string username)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "generalRole"));
            if (username != null)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
            }
            return claims;
        }
    }
}
