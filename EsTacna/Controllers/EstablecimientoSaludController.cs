using EsTacna.Models;
using EsTacna.Repositories;
using EsTacna.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EsTacna.Controllers
{
    public class EstablecimientoSaludController : Controller
    {
        private readonly EstablecimientoSaludRepositoryImpl objEstablecimientoRepo = new EstablecimientoSaludRepositoryImpl(new EsTacnaContext());
        private readonly BusquedaRepositoryImpl objBusquedaRepo = new BusquedaRepositoryImpl(new EsTacnaContext());
        private readonly EpsRepositoryImpl objEpsRepo = new EpsRepositoryImpl(new EsTacnaContext());
        private readonly EpsEstablecimientoSaludRepositoryImpl objEpsEstablecimientoRepo = new EpsEstablecimientoSaludRepositoryImpl(new EsTacnaContext());
        private readonly ValoracionRepositoryImpl objValoracionRepo = new ValoracionRepositoryImpl(new EsTacnaContext());
        public IActionResult Buscar(string criterio, int epsid)
        {
            Busquedum objBuscar = new Busquedum();
            List<EstablecimientoSaludViewModel> listEstablecimientoVm = new List<EstablecimientoSaludViewModel>();
            var listEstablecimiento = new List<EstablecimientoSalud>();
            if (criterio == "" || criterio == null)
            {
                listEstablecimiento = objEstablecimientoRepo.Listar(epsid).ToList();
            }
            else
            {
                listEstablecimiento = objEstablecimientoRepo.Buscar(criterio, epsid).ToList();
            }
            foreach (var item in listEstablecimiento)
            {
                EstablecimientoResponse objEstablecimientoResponse = new EstablecimientoResponse();
                EstablecimientoSaludViewModel objEstablecimientoVm = new EstablecimientoSaludViewModel();
                objEstablecimientoVm.establecimientoSalud = item;
                objEstablecimientoVm.eps = objEpsRepo.BuscarId(objEpsEstablecimientoRepo.BuscarId(item.Id).EpsId);
                objEstablecimientoVm.Clasificacion = objEstablecimientoResponse.ObtenerEstablecimiento(item.Id).Result.clasificacionReal;
                listEstablecimientoVm.Add(objEstablecimientoVm);
            }
            objBuscar.TerminoBusqueda = objEpsRepo.BuscarId(epsid).Nombre + " " + criterio;
            objBuscar.UsuarioId = Convert.ToInt32(HttpContext.Session.GetString("UsuarioId") ?? "1");
            objBuscar.Fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            objBusquedaRepo.Registrar(objBuscar);
            return View(listEstablecimientoVm);
        }
        public IActionResult Detalle(int EstablecimientoId)
        {
            EstablecimientoSaludViewModel objEstablecimientoVm = new EstablecimientoSaludViewModel();
            var idEstablecimiento = objEstablecimientoRepo.BuscarId(EstablecimientoId);
            objEstablecimientoVm.establecimientoSalud = idEstablecimiento;
            objEstablecimientoVm.eps = objEpsEstablecimientoRepo.BuscarIdEps(EstablecimientoId);
            objEstablecimientoVm.listValoracion = objValoracionRepo.ListarPorEstablecimientoId(idEstablecimiento.Id);
            objEstablecimientoVm.TotalValoraciones = (objEstablecimientoVm.listValoracion.Count() == 0) ? 0 : Convert.ToInt32(objEstablecimientoVm.listValoracion.Sum(x => x.Calificacion) / objEstablecimientoVm.listValoracion.Count());
            return View(objEstablecimientoVm);
        }
        [HttpPost]
        public IActionResult Valorar(Valoracion objValoracion)
        {
            objValoracionRepo.Guardar(objValoracion);
            return RedirectToAction("Detalle", new { idEstablecimiento = objValoracion.EstablecimientoId }); ;
        }
    }
}
