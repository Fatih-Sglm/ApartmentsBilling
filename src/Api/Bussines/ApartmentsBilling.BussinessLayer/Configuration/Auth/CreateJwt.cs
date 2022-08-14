using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Entity.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApartmentsBilling.BussinessLayer.Configuration.Auth
{
    public class CreateJwt
    {
        public static TokenDto GetJtwtToken(IConfiguration configuration, User user)
        {
            var claims = new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FullName),
                    new Claim(ClaimTypes.Role,user.Role.ToString())
            };
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOption>();


            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
            var jwtToken = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new()
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                accessTokenExpiration = tokenOptions.AccessTokenExpiration
            };
        }
    }
}
