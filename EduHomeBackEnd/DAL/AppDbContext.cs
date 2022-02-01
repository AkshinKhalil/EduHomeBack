using EduHomeBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHomeBackEnd.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<TeacherHobby> TeacherHobbies { get; set; }
        public DbSet<WelcomeSection> WelcomeSections { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseFeatures> CourseFeatures { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
    }

}
