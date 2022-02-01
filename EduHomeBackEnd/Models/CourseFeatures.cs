using System;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEnd.Models
{
    public class CourseFeatures
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Starts { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Duration { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string ClassDuration { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string SkillLevel { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Language { get; set; }
        [Required]
        public int StudentsCount { get; set; }
        public int CourseId { get; set; }
        public  Course Course { get; set; }
        
    }
}
