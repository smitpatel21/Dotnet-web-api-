using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var result = await _accountRepository.SignupAsync(signupModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SigninModel signinModel)
        {
            string token = await _accountRepository.LoginAsync(signinModel);
            
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            var user = await  _accountRepository.GetUser(signinModel.Email);
            return Ok(new {token,user});
        }

        [HttpGet("logout")]
        public async Task Logout()
        {
            await _accountRepository.SignOut();
        }

        [HttpPost("change-password/{id}")]
        public async Task ChangePassword([FromRoute]string id,[FromBody] ChangePasswordModel changePasswordModel)
        {
           await _accountRepository.ChangePasswordAsync(id,changePasswordModel);
        }
    }
}
