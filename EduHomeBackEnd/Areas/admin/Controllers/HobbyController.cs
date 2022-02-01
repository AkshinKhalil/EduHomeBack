using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEnd.Areas.admin.Controllers
{
    [Area("admin")]
    public class HobbyController : Controller
    {
        private readonly AppDbContext _context;
        public HobbyController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Hobby> model = _context.Hobbies.Include(h => h.TeacherHobbies).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Hobbies.Add(hobby);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Hobby hobby = _context.Hobbies.FirstOrDefault(h => h.Id == id);
            return View(hobby);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Hobby existedHobby= _context.Hobbies.FirstOrDefault(h => h.Id == hobby.Id);
            if (existedHobby == null)
            {
                return NotFound();
            }
            Hobby sameName = _context.Hobbies.FirstOrDefault(h => h.Name.ToLower().Trim() == hobby.Name.ToLower().Trim());
            if (sameName != null)
            {
                ModelState.AddModelError("", "This hobby name is available in the database");
                return View();
            }
            existedHobby.Name = hobby.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Hobby hobby = _context.Hobbies.FirstOrDefault(h => h.Id == id);
            if (hobby == null) return Json(new { status = 404 });

            _context.Hobbies.Remove(hobby);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}