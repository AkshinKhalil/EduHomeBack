using EduHomeBackEnd.Models;
using System.Collections.Generic;

namespace EduHomeBackEnd.ViewModels
{
    public class BlogVM
    {
        public Blog Blog { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
