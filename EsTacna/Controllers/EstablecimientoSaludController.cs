using EsTacna.Models;
using EsTacna.Repositories;
using EsTacna.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EsTacna.Controllers
{
    public class EstablecimientoSaludController : Controller
    {
        private readonly EstablecimientoSaludRepositoryImpl estrepo = new EstablecimientoSaludRepositoryImpl(new EsTacnaContext());
        private readonly BusquedaRepositoryImpl busqrepo = new BusquedaRepositoryImpl(new EsTacnaContext());
        private readonly EpsRepositoryImpl epsrepo = new EpsRepositoryImpl(new EsTacnaContext());
        private readonly EpsEstablecimientoSaludRepositoryImpl epsestrepo = new EpsEstablecimientoSaludRepositoryImpl(new EsTacnaContext());
        private readonly ValoracionRepositoryImpl valrepo = new ValoracionRepositoryImpl(new EsTacnaContext());
        public IActionResult Buscar(string criterio, int epsid)
        {
            Busquedum objBusc = new Busquedum();
            List<EstablecimientoSaludViewModel> listEstvm = new List<EstablecimientoSaludViewModel>();
            var listEst = new List<EstablecimientoSalud>();
            var listEpsEst = new List<EpsEstablecimientoSalud>();
            if (criterio == "" || criterio == null)
            {
                listEst = estrepo.Listar(epsid).ToList();
            }
            else
            {
                listEst = estrepo.Buscar(criterio, epsid).ToList();
            }
            foreach (var item in listEst)
            {
                EstablecimientoResponse objEstResp = new EstablecimientoResponse();
                EstablecimientoSaludViewModel objEstvm = new EstablecimientoSaludViewModel();
                objEstvm.estSalud = item;
                objEstvm.eps = epsrepo.BuscarId(epsestrepo.BuscarId(item.Id).EpsId);
                objEstvm.Clasificacion = objEstResp.ObtenerEstablecimiento(item.Id).Result.clasfreal;
                listEstvm.Add(objEstvm);
            }
            objBusc.TerminoBusqueda = epsrepo.BuscarId(epsid).Nombre + " " + criterio;
            objBusc.UsuarioId = Convert.ToInt32(HttpContext.Session.GetString("UsuarioId") ?? "1");
            objBusc.Fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            busqrepo.Registrar(objBusc);
            return View(listEstvm);
        }
        public IActionResult Detalle(int EstId)
        {
            EstablecimientoSaludViewModel objEstvm = new EstablecimientoSaludViewModel();
            Valoracion objVal = new Valoracion();
            var IdEst = estrepo.BuscarId(EstId);
            objEstvm.estSalud = IdEst;
            objEstvm.eps = epsestrepo.BuscarIdEps(EstId);
            objEstvm.listValoracion = valrepo.ListarPorEstablecimientoId(IdEst.Id);
            objEstvm.TotalValoraciones = (objEstvm.listValoracion.Count() == 0) ? 0 : Convert.ToInt32(objEstvm.listValoracion.Sum(x => x.Calificacion) / objEstvm.listValoracion.Count());
            return View(objEstvm);
        }
        [HttpPost]
        public IActionResult Valorar(Valoracion objVal)
        {
            //objVal.Guardar();
            valrepo.Guardar(objVal);
            return RedirectToAction("Detalle", new { EstId = objVal.EstablecimientoId }); ;
        }
    }
}
