using EsTacna.Models;
using EsTacna.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsTacna.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositoryImpl usrepo = new UsuarioRepositoryImpl(new EsTacnaContext());
        private readonly UnitOfWork useruni = new UnitOfWork(new EsTacnaContext());
        // GET: Usuario/Perfil
        public IActionResult Perfil()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }
        // POST: Usuario/Registrar
        [HttpPost]
        public IActionResult Registrar(Usuario objUsu)
        {
            if (ModelState.IsValid)
            {
                usrepo.Registrar(objUsu);
                useruni.SaveChanges();
                return View(objUsu);
            }
            else
            {
                return View(objUsu);
            }
        }
        // GET: Usuario/Perfil?idUs=1
        [HttpGet]
        public IActionResult Perfil(int idUs)
        {
            var resultado = usrepo.BuscarId(idUs);
            return View(resultado);
        }

        // POST: Usuario/Perfil
        [HttpPost]
        public IActionResult Perfil(Usuario objUsu)
        {
            usrepo.Registrar(objUsu);
            useruni.SaveChanges();
            return Redirect("~/Usuario/Perfil?idUs=" + objUsu.Id);
        }
    }
}
