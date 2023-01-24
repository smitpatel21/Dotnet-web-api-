using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IUserRepository
    {
        List<ApplicationUser> GetAllUsers();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task UpdateUser(string id, ApplicationUser applicationUser);
        Task DeleteUser(string id);
    }
}
