using Microsoft.AspNetCore.Mvc;

namespace SistemaReclamo.Controllers
{
    public class ListaReclamoController : Controller
    {
        public IActionResult ListaReclamo()
        {
            return View();
        }
    }
}
