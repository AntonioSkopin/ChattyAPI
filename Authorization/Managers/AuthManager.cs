using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChattyAPI.Authorization.Contracts;
using ChattyAPI.Authorization.DTO;
using ChattyAPI.Authorization.Models;
using ChattyAPI.Base.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChattyAPI.Authorization.Managers
{
    public class AuthManager : BaseManager, IAuthManager
    {
        private readonly UserManager<ChattyUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IConfiguration configuration, UserManager<ChattyUser> userManager) : base(configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResultDTO> Login(LoginInfo loginInfo)
        {
            var user = await _userManager.FindByNameAsync(loginInfo.username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginInfo.password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, GenerateGd().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(6),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                LoginResultDTO loginResult = new LoginResultDTO();
                loginResult.gd = user.gd;
                loginResult.token = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };

                return loginResult;
            }
            return null;
        }
    }
}