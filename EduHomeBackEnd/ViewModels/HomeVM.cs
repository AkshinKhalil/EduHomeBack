using EduHomeBackEnd.Models;
using System.Collections.Generic;

namespace EduHomeBackEnd.ViewModels
{
    public class HomeVM
    {
        public List<Hobby> Hobbies { get; set; }
        public List<Teacher> Teachers { get; set; }
        public WelcomeSection WelcomeSection { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Course> Courses { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Event> Events { get; set; }
        public List<Comment> Comments { get; set; }
        public Setting Setting { get; set; }
    }
}
