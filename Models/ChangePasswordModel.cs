using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ChangePasswordModel
    {
        [Required,DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set;}
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set;}
    }
}
