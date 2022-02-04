using Microsoft.AspNetCore.Mvc;

namespace EduHomeBackEnd.Controllers
{
    public class AccountController1 : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
