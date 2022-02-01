using EduHomeBackEnd.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduHomeBackEnd.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    //Setting setting = _context.Settings.FirstOrDefault();
        //    //return View(await Task.FromResult(setting));
        //}
    }
}
