using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEnd.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 90)]
        public string Venue { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndTime { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public int ImageIds { get; set; }
        [NotMapped]
        public List<int> SpeakersIds { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
