using Microsoft.AspNetCore.Mvc;
using SistemaReclamo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace SistemaReclamo.Controllers
{
    public class LoginController : Controller
    {

        private readonly DbtempMuñozContext _Dbcontext;

        public LoginController(DbtempMuñozContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
      
        [HttpGet]
        public IActionResult LoginDC()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public IActionResult LoginDC(LoginViewModel model)
        {
            if(string.IsNullOrEmpty(model.Usuario) || string.IsNullOrEmpty(model.Clave))
            {
                model.ErrorMessage = "usuario y clave son requeridos";
                
            }
            var empleado = _Dbcontext.CEmpleados.FirstOrDefault(e => e.Usuario == model.Usuario && e.Clave == model.Clave && e.Activo==true);
            if(empleado != null)
            {
                // guarda los datos para sesion
                HttpContext.Session.SetInt32("IdEmpleado", empleado.IdEmpleado);
                HttpContext.Session.SetString("NombreEmpleado", empleado.Nombres ?? "");
                HttpContext.Session.SetString("ApellidosEmpleado", empleado.Apellidos ?? "");
                //redirigir a la pagina principal
                return RedirectToAction("ListaReclamo", "Reclamo");
            }
            else
            {
                model.ErrorMessage = "Usuario o clave incorrectos o el usuario no esta activo";

                return View(model);
            }


               
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginDC", "Login");
        }

    }
}
