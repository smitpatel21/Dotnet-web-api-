﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{

    public class Skills
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
    }

}
