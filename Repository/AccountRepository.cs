using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.Helper;
using WebAPI.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BookStoreContext _context;
        private readonly IConfiguration _configuration;

        public AccountRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            BookStoreContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IdentityResult> SignupAsync(SignupModel signupModel)
        {
            var user = new ApplicationUser()
            {
                UserName = signupModel.Email,
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                Role = signupModel.Role,
            };

            var res1 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            var res2 = await _roleManager.CreateAsync(new IdentityRole("Customer"));
            var result = await _userManager.CreateAsync(user, signupModel.Password);
            if (result.Succeeded)
            {
                if (signupModel.Role == "Customer")
                    await _userManager.AddToRoleAsync(user, "Customer");
                else
                    await _userManager.AddToRoleAsync(user, "Admin");
            }
            return result;
        }

        public async Task<string> LoginAsync(SigninModel signinModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signinModel.Email, signinModel.Password, true, false);
            if (!result.Succeeded) return null;
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,signinModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApplicationUser> GetUser(string email)
        {
            var x = await  _context.ApplicationUser.Include(x => x.skills).Include(x=>x.favourites)
                .Where(x => x.Email == email).FirstOrDefaultAsync();
            return x;
            //return await _userManager.FindByEmailAsync(email);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task ChangePasswordAsync(string userId, ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
        }
    }
}
