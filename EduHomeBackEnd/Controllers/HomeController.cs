using EduHomeBackEnd.DAL;
using EduHomeBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM()
            {
                Teachers = _context.Teachers.Include(t => t.TeacherHobbies).ToList(),
                WelcomeSection = _context.WelcomeSections.FirstOrDefault(),
                Sliders=_context.Sliders.ToList(),
                Courses = _context.Courses.ToList(),
                Blogs = _context.Blogs.ToList(),
                Events = _context.Events.ToList(),
                Comments = _context.Comments.ToList(),
                Setting = _context.Settings.FirstOrDefault()
            };

            return View(model);
        }
    }
}
