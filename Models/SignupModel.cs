using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class SignupModel
    {
        [Required]
        public string FirstName { get; set;}
        [Required]
        public string LastName { get; set;}
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set;} 
        [Required]
        public string ConfirmPassword { get; set; }
         
    }
}
