using System;
using System.Threading.Tasks;
using ChattyAPI.Authorization.Contracts;
using ChattyAPI.Authorization.DTO;
using ChattyAPI.Authorization.Models;
using ChattyAPI.Base.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChattyAPI.Authorization.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly UserManager<ChattyUser> _userManager;

        public AuthController(IAuthManager authManager, IConfiguration configuration, UserManager<ChattyUser> userManager)
        {
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResultDTO>> Login([FromBody] LoginInfo loginInfo)
        {
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new Response { Status = "Error!", Message = "User can't log in!" });
            }
            var loginResult = await _authManager.Login(loginInfo);
            if (loginResult == null)
            {
                return Unauthorized(new Response { Status = "Unauthorized!", Message = "Invalid user details!" });
            }
            return Ok(loginResult);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var userExists = await _userManager.FindByNameAsync(model.username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ChattyUser user = new ChattyUser()
            {
                gd = Guid.NewGuid(),
                UserName = model.username,
                fullname = model.fullname,
                Email = model.email,
                bio = model.bio,
                birthdate = model.birthdate,
            };

            var result = await _userManager.CreateAsync(user, model.password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}