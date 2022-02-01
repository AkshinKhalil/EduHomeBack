using EduHomeBackEnd.Models;
using System.Collections.Generic;

namespace EduHomeBackEnd.ViewModels
{
    public class HomeVM
    {
        public List<Hobby> Hobbies { get; set; }
        public List<Teacher> Teachers { get; set; }
        public WelcomeSection WelcomeSection { get; set; }
    }
}
