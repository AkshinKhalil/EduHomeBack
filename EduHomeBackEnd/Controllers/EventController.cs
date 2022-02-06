using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Models;
using EduHomeBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduHomeBackEnd.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Event> events = _context.Events.ToList();
            return View(events);
        }
        public IActionResult Details(int id)
        {
            EventVM model = new EventVM()
            {
                Event = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker).FirstOrDefault(c => c.Id == id),
                Tags = _context.Tags.ToList(),
                EventSpeakers=_context.EventSpeakers.ToList(),
                Speakers=_context.Speakers.ToList()
            };
            return View(model);
        }
    }
}
