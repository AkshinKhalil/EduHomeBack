using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEnd.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Profession { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
