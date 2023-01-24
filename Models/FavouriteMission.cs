using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class FavouriteMission
    {
        [Key]
        public int Id { get; set; }
        public int MissionId { get; set; }
        public string UserId { get; set; }

    }
}
