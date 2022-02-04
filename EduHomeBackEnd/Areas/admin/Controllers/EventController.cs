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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 1);

            List<Event> events = _context.Events.Skip((page - 1) * 2).Take(2).ToList();
            return View(events);
        }
        public IActionResult Create()
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event eventt)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            if (!ModelState.IsValid) return View();

            eventt.EventSpeakers = new List<EventSpeaker>();
            foreach (int id in eventt.SpeakersIds)
            {
                EventSpeaker eSpeaker = new EventSpeaker
                {
                    Event = eventt,
                    SpeakerId = id
                };
                eventt.EventSpeakers.Add(eSpeaker);
            }
            if (!eventt.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFiles", "Please choose image file");
                return View();
            }
            if (!eventt.ImageFile.IsSizeOkay(2))
            {
                ModelState.AddModelError("ImageFiles", "Image size must be max 2MB");
                return View();
            }
            eventt.Image = eventt.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");
            _context.Events.Add(eventt);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            ViewBag.Speakers = _context.Speakers.ToList();

            Event eventt = _context.Events.Include(t => t.EventSpeakers).FirstOrDefault(f => f.Id == id);
            if (eventt == null) return NotFound();

            return View(eventt);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event eventt)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            Event existedEvent = _context.Events.Include(e => e.EventSpeakers).FirstOrDefault(f => f.Id == eventt.Id);


            if (!ModelState.IsValid) return View(existedEvent);

            if (existedEvent == null) return NotFound();

            if (eventt.ImageFile != null)
            {
                if (!eventt.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please select the image file");
                    return View(existedEvent);
                }
                if (!eventt.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFiles", "You can choose file which size is max 2MB");
                    return View(existedEvent);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", existedEvent.Image);
                existedEvent.Image = eventt.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");

                 List<EventSpeaker> removeSpeaker = existedEvent.EventSpeakers.Where(es => !eventt.SpeakersIds.Contains(es.Id)).ToList();
                existedEvent.EventSpeakers.RemoveAll(es => removeSpeaker.Any(rs => es.Id == rs.Id));
                foreach (var speakerId in eventt.SpeakersIds)
                {
                    EventSpeaker eventSpeaker = existedEvent.EventSpeakers.FirstOrDefault(es => es.SpeakerId == speakerId);
                    if (eventSpeaker == null)
                    {
                        EventSpeaker eSpeaker = new EventSpeaker
                        {
                            SpeakerId = speakerId,
                            EventId = existedEvent.Id
                        };
                        existedEvent.EventSpeakers.Add(eSpeaker);
                    }
                }
            }
            
            existedEvent.Name = eventt.Name;
            existedEvent.Description = eventt.Description;
            existedEvent.Venue = eventt.Venue;
            existedEvent.StartTime = eventt.StartTime;
            existedEvent.EndTime = eventt.EndTime;           

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Event eventt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventt == null) return Json(new { status = 404 });

            _context.Events.Remove(eventt);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
