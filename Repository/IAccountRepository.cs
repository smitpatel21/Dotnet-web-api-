using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignupAsync(SignupModel signupModel);
        Task<string> LoginAsync(SigninModel signinModel);
        Task<ApplicationUser> GetUser(string email);
        Task ChangePasswordAsync(string userId, ChangePasswordModel changePasswordModel);
        Task SignOut();
    }
}
