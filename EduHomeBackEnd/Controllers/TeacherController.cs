using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Models;
using EduHomeBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EduHomeBackEnd.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM()
            {
            Hobbies = _context.Hobbies.ToList(),
                Teachers = _context.Teachers.Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobby).ToList()
            };
            return View(model);
        }

        public IActionResult Details(int id)
        {
            Teacher teacher = _context.Teachers.Include(t => t.TeacherHobbies).ThenInclude(th => th.Hobby).FirstOrDefault(t => t.Id == id);
            //return Content(id.ToString());
           return View(teacher);
        }
    }
}
