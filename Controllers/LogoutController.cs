using Microsoft.AspNetCore.Mvc;

namespace SistemaReclamo.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Logout()
        {
            return View();
        }
    }
}
