using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Extensions;
using EduHomeBackEnd.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEnd.Areas.admin.Controllers
{
    [Area("admin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Teacher> flowers = _context.Teachers.ToList();
            return View(flowers);
        }
        public IActionResult Create()
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            if (!ModelState.IsValid) return View();

            teacher.TeacherHobbies = new List<TeacherHobby>();
            foreach (int id in teacher.HobbiesIds)
            {
                TeacherHobby tHobby = new TeacherHobby
                {
                    Teacher = teacher,
                    HobbyId = id
                };
                teacher.TeacherHobbies.Add(tHobby);
            }
                if (!teacher.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please choose image file");
                    return View();
                }
                if (!teacher.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFiles", "Image size must be max 2MB");
                    return View();
                }
            teacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();

            Teacher teacher = _context.Teachers.Include(t => t.TeacherHobbies).FirstOrDefault(f => f.Id == id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Teacher teacher)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            Teacher existedTeacher = _context.Teachers.Include(t => t.TeacherHobbies).FirstOrDefault(f => f.Id == teacher.Id);


            if (!ModelState.IsValid) return View(existedTeacher);

            if (existedTeacher == null) return NotFound();

            if (teacher.ImageFile != null)
            {                
                    if (!teacher.ImageFile.IsImage())
                    {
                        ModelState.AddModelError("ImageFiles", "Please select the image file");
                        return View(existedTeacher);
                    }
                    if (!teacher.ImageFile.IsSizeOkay(2))
                    {
                        ModelState.AddModelError("ImageFiles", "You can choose file which size is max 2MB");
                        return View(existedTeacher);
                    }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", existedTeacher.Image);
                existedTeacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");

                List<TeacherHobby> removeHobby = existedTeacher.TeacherHobbies.Where(fc => !teacher.HobbiesIds.Contains(fc.Id)).ToList();

                existedTeacher.TeacherHobbies.RemoveAll(th => removeHobby.Any(rh => th.Id == rh.Id));
                foreach (var hobbyId in teacher.HobbiesIds)
                {
                    TeacherHobby teacherHobby = existedTeacher.TeacherHobbies.FirstOrDefault(th => th.HobbyId == hobbyId);
                    if (teacherHobby == null)
                    {
                        TeacherHobby tHobby = new TeacherHobby
                        {
                            HobbyId = hobbyId,
                            TeacherId = existedTeacher.Id
                        };
                        existedTeacher.TeacherHobbies.Add(tHobby);
                    }
                }
            }
            
            existedTeacher.Name = teacher.Name;
            existedTeacher.Degree = teacher.Degree;
            existedTeacher.Description = teacher.Description;
            existedTeacher.Email = teacher.Email;
            existedTeacher.Number = teacher.Number;
            existedTeacher.Profession = teacher.Profession;
            existedTeacher.FacebookURL = teacher.FacebookURL;
            existedTeacher.Faculty = teacher.Faculty;
            existedTeacher.Experience = teacher.Experience;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null) return Json(new { status = 404 });

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
