using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Models;
using EduHomeBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEnd.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Course> courses = _context.Courses.ToList();
            return View(courses);
        }
        public IActionResult Details(int id)
        {
            CourseVM model = new CourseVM()
            {
                Course = _context.Courses.Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Include(c => c.CourseFeatures).FirstOrDefault(c => c.Id == id),
                Tags = _context.Tags.ToList(),
                CourseFeatures=_context.CourseFeatures.FirstOrDefault(),
                Categories=_context.Categories.ToList()
            };
            return View(model);
        }

        public IActionResult Search(string search)
        {
            if (search == null) return NotFound();
            List<Course> model = _context.Courses.Where(c => c.Name.Contains(search)).Take(8).OrderByDescending(c => c.Id).ToList();
            return View(model);
        }
    }
}
