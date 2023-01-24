using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public IFormFile? profilePic { get; set; }
        public string? profilePicUrl { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public string? status { get; set; }
        public string? linkedin { get; set; }
        public string? about { get; set; }
        public string? location { get; set; }
        public List<Skills> skills { get; set; }
        public List<FavouriteMission> favourites { get; set; }
    }
}
