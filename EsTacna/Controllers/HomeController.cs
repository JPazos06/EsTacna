using EsTacna.Models;
using EsTacna.Repositories;
using EsTacna.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EsTacna.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EstablecimientoSaludRepositoryImpl objEstablecimientoRepo = new EstablecimientoSaludRepositoryImpl(new EsTacnaContext());
        private readonly EpsRepositoryImpl objEpsRepo = new EpsRepositoryImpl(new EsTacnaContext());
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            EstablecimientoSaludViewModel objEstablecimientoVm = new EstablecimientoSaludViewModel();
            EstablecimientoResponse objEstablecimientoResponse = new EstablecimientoResponse();
            objEstablecimientoVm.listEstablecimiento = objEstablecimientoRepo.ListarMap();
            objEstablecimientoVm.listEps = objEpsRepo.Listar();
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                var idUsuario = HttpContext.Session.GetString("UsuarioId");
                objEstablecimientoVm.RecoEstablecimiento = objEstablecimientoResponse.GetEstablecimiento(Convert.ToInt32(idUsuario)).Result;
            }
            return View(objEstablecimientoVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
