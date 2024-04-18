using EsTacna.Models;
using EsTacna.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsTacna.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositoryImpl objUsuarioRepo = new UsuarioRepositoryImpl(new EsTacnaContext());
        private readonly UnitOfWork objUsuarioUnit = new UnitOfWork(new EsTacnaContext());
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
        public IActionResult Registrar(Usuario objUsuario)
        {
            try
            {
                objUsuarioRepo.Registrar(objUsuario);
                objUsuarioUnit.SaveChanges();
                return View(objUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al registrar el usuario.", ex);
            }
        }
        // GET: Usuario/Perfil?idUs=1
        [HttpGet]
        public IActionResult Perfil(int idUsuario)
        {
            var resultado = objUsuarioRepo.BuscarId(idUsuario);
            return View(resultado);
        }

        // POST: Usuario/Perfil
        [HttpPost]
        public IActionResult Perfil(Usuario objUsuario)
        {
            objUsuarioRepo.Registrar(objUsuario);
            objUsuarioUnit.SaveChanges();
            return Redirect("~/Usuario/Perfil?idUs=" + objUsuario.Id);
        }
    }
}
