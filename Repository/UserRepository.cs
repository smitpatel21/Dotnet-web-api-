using Microsoft.AspNetCore.Identity;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BookStoreContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager,BookStoreContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            var records = _userManager.Users.Where(x => x.Role == "Customer").Select(
                x => new ApplicationUser() {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,  
                    Email = x.Email,
                    Role = x.Role,
                }).ToList();
            return records;
        }
        

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task UpdateUser(string id,ApplicationUser applicationUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Email = applicationUser.Email;
            user.FirstName = applicationUser.FirstName;
            user.UserName = applicationUser.Email;
            user.LastName = applicationUser.LastName;
            user.profilePicUrl = applicationUser.profilePicUrl;
            user.Id = id;
            user.status = applicationUser.status;
            user.city = applicationUser.city;
            user.country = applicationUser.country;
            user.linkedin = applicationUser.linkedin;
            user.about = applicationUser.about;
            var skillList = _context.skills.Where(x=>x.UserId==id).Select(x=>new Skills()
            {
                Id = x.Id,
                Name = x.Name,
                UserId = x.UserId,
            }).ToList();
            _context.skills.RemoveRange(skillList);
            _context.SaveChanges();
            _context.skills.AddRange(applicationUser.skills);
            user.skills = applicationUser.skills;
            await _context.SaveChangesAsync();
            await _userManager.UpdateAsync(user);    
        }

        public async Task DeleteUser(string id)
        {
            var user=await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user); 
        }
    }
}
