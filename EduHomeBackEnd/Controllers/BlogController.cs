using EduHomeBackEnd.DAL;
using EduHomeBackEnd.Models;
using EduHomeBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackEnd.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BlogController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.Include(b=>b.Comments).ToList();
            return View(blogs);
        }
        public IActionResult Details(int id)
        {
            BlogVM model = new BlogVM()
            {
                Blog = _context.Blogs.FirstOrDefault(b => b.Id == id),
                Tags = _context.Tags.ToList(),
                Comments = _context.Comments.ToList()
            };
            return View(model);
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
            if (!_context.Blogs.Any(f => f.Id == comment.BlogId)) return NotFound();
            Comment cmnt = new Comment
            {
                Text = comment.Text,
                BlogId = comment.BlogId,
                CreatedTime = DateTime.Now,
                AppUserId = user.Id
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }

       
    }
}
