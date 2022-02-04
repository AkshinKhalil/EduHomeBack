using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Extensions;
using EduHomeBackEnd.Models;
using EduHomeBackEnd.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEnd.Areas.admin.Controllers
{
    [Area("admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Course> courses = _context.Courses.ToList();
            return View(courses);
        }
        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            if (!ModelState.IsValid) return View();

            course.CourseTags = new List<CourseTag>();
            foreach (int id in course.TagIds)
            {
                CourseTag cTag = new CourseTag
                {
                    Course = course,
                    TagId = id
                };
                course.CourseTags.Add(cTag);
            }
            if (!course.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFiles", "Please choose image file");
                return View();
            }
            if (!course.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFiles", "Image size must be max 2MB");
                return View();
            }
            course.Image = course.ImageFile.SaveImg(_env.WebRootPath, "assets/img/course");
            course.CourseFeatures.Course = course;
            course.CourseFeatures.CourseId = course.Id;
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseTags = _context.CourseTags.Include(ct => ct.Course).Include(ct=>ct.Tag).FirstOrDefault(ct=>ct.CourseId==id);
            
            if (!ModelState.IsValid) return NotFound();

            Course course = _context.Courses.Include(c=>c.CourseFeatures).FirstOrDefault(c => c.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseTags = _context.CourseTags.Include(ct => ct.Course).Include(ct => ct.Tag).FirstOrDefault(ct => ct.CourseId == course.Id);
            Course existedCourse = _context.Courses.FirstOrDefault(c => c.Id == course.Id);


            if (!ModelState.IsValid) return View(existedCourse);

            if (existedCourse == null) return NotFound();

            if (course.ImageFile != null)
            {
                if (!course.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please select the image file");
                    return View(existedCourse);
                }
                if (!course.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFiles", "You can choose file which size is max 2MB");
                    return View(existedCourse);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/course", existedCourse.Image);
                existedCourse.Image = course.ImageFile.SaveImg(_env.WebRootPath, "assets/img/course");

               // List<CourseTag> removeTag = existedCourse.CourseTags.Where(ct => !course.TagIds.Contains(ct.Id)).ToList();

               // existedCourse.CourseTags.RemoveAll(ct => removeTag.Any(rt => ct.Id == rt.Id));
                foreach (var tagid in course.TagIds)
                {
                    CourseTag courseTag = existedCourse.CourseTags.FirstOrDefault(ct => ct.TagId == tagid);
                    if (courseTag == null)
                    {
                        CourseTag cTag = new CourseTag
                        {
                            TagId = tagid,
                            CourseId = existedCourse.Id
                        };
                        existedCourse.CourseTags.Add(cTag);
                    }
                }
            }

            existedCourse.Name = course.Name;
            existedCourse.AboutCourse = course.AboutCourse;
            existedCourse.Description = course.Description;
            existedCourse.Apply = course.Apply;
            existedCourse.Certification = course.Certification;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
