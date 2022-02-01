using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEnd.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Description { get; set; }
        public string Image { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string AboutCourse { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Apply { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Certification { get; set; }

        public  CourseFeatures CourseFeatures { get; set; }
    }
}
