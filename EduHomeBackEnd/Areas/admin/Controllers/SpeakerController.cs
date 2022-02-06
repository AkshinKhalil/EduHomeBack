using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Extensions;
using EduHomeBackEnd.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEnd.Areas.admin.Controllers
{
    [Area("admin")]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SpeakerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Speakers.Count() / 1);

            List<Speaker> speakers = _context.Speakers.Skip((page - 1) * 2).Take(2).ToList();
            return View(speakers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Speaker speaker)
        {            
            if (!ModelState.IsValid) return View();
            speaker.EventSpeakers = new List<EventSpeaker>();           
            if (!speaker.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFiles", "Please choose image file");
                return View();
            }
            if (!speaker.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFiles", "Image size must be max 2MB");
                return View();
            }
            speaker.Image = speaker.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");
            _context.Speakers.Add(speaker);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (speaker == null) return NotFound();

            return View(speaker);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Speaker speaker)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            Speaker existedSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == speaker.Id);


            if (!ModelState.IsValid) return View(existedSpeaker);

            if (existedSpeaker == null) return NotFound();

            if (speaker.ImageFile != null)
            {
                if (!speaker.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please select the image file");
                    return View(existedSpeaker);
                }
                if (!speaker.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFiles", "You can choose file which size is max 2MB");
                    return View(existedSpeaker);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existedSpeaker.Image);
                existedSpeaker.Image = speaker.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");               
            }

            existedSpeaker.Name = speaker.Name;
            existedSpeaker.Profession = speaker.Profession;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (speaker == null) return Json(new { status = 404 });

            _context.Speakers.Remove(speaker);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
