using EsTacna.Models;
using EsTacna.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsTacna.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioRepositoryImpl usrepo = new UsuarioRepositoryImpl(new EsTacnaContext());
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario objU)
        {
            Usuario logUsuario = usrepo.Login(objU.Email, objU.Contrasena);

            if (logUsuario != null)
            {
                HttpContext.Session.SetString("UsuarioNombre", logUsuario.Nombre);
                HttpContext.Session.SetString("UsuarioId", logUsuario.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Inicio de sesión no válido, mostrar mensaje de error o redirigir a una página de error de inicio de sesión
                return View();
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
