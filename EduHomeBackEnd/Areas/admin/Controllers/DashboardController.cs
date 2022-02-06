using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEnd.Areas.admin.Controllers
{
    [Area("admin")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
    }
}
