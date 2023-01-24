using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class MissionModel
    {
        [Key]
        public int MissionId { get; set; }
        public string MissionName { get; set; }
        public string MissionTitle { get; set; }
        public string MissionDescription { get; set; }
        public int AvailableSeats { get; set; }
        public string StartDate { get; set; }
        public string Deadline { get; set; }
        public string Location { get; set; }
        [NotMapped]
        public IFormFile missionPic { get; set; }
        public string missionPicUrl { get; set; }
    }
}
