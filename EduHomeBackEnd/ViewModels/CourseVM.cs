using EduHomeBackEnd.Models;
using System.Collections.Generic;

namespace EduHomeBackEnd.ViewModels
{
    public class CourseVM
    {
        public Course Course { get; set; }
        public CourseFeatures CourseFeatures { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
