using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEnd.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Name { get; set; }
        public List<CourseTag> CourseTags { get; set; }
    }
}
