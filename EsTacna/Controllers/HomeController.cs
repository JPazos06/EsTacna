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
        private readonly EstablecimientoSaludRepositoryImpl estrepo = new EstablecimientoSaludRepositoryImpl(new EsTacnaContext());
        private readonly EpsRepositoryImpl eprepo = new EpsRepositoryImpl(new EsTacnaContext());
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            EstablecimientoSaludViewModel objEstvm = new EstablecimientoSaludViewModel();
            EstablecimientoResponse objResp = new EstablecimientoResponse();
            objEstvm.listEst = estrepo.ListarMap();
            objEstvm.listEps = eprepo.Listar();
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                var idUs = HttpContext.Session.GetString("UsuarioId");
                objEstvm.RecEst = objResp.GetEstablecimiento(Convert.ToInt32(idUs)).Result;
            }
            return View(objEstvm);
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
