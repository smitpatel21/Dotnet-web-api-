using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebAPI.Models;

namespace WebAPI.Helper
{
     
        public class Claims : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
        {
            public Claims(UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
            {

            }
            protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
            {
                var identity = await base.GenerateClaimsAsync(user);
                identity.AddClaim(new Claim("Id", user.Id ?? ""));
                identity.AddClaim(new Claim("Email", user.Email ?? ""));
                return identity;
            }
        }
    }

