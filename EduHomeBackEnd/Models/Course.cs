using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int CategoryId { get; set; }   
        public  CourseFeatures CourseFeatures { get; set; }
        public Category Category { get; set; }
        public List<CourseTag> CourseTags { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
