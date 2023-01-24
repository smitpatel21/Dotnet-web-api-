using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser> 
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options):base(options)
        {

        }
        public DbSet<Skills> skills { get; set; }
        public DbSet<FavouriteMission> favouriteMissions { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<MissionModel> Missions { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
