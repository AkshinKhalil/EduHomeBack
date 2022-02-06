using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEnd.Areas.admin.Controllers
{
    [Area("admin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Tag> model = _context.Tags.Include(t => t.CourseTags).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Tag existedTag = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (existedTag == null)
            {
                return NotFound();
            }
            Tag sameName = _context.Tags.FirstOrDefault(t => t.Name.ToLower().Trim() == tag.Name.ToLower().Trim());
            if (sameName != null)
            {
                ModelState.AddModelError("", "This tag name is available in the database");
                return View();
            }
            existedTag.Name = tag.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return Json(new { status = 404 });

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}