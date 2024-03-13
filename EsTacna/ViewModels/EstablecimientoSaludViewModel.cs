using EsTacna.Models;

namespace EsTacna.ViewModels
{
    public class EstablecimientoSaludViewModel
    {
        public EstablecimientoSalud estSalud { get; set; }
        public Valoracion calificacion { get; set; }
        public List<Valoracion> listValoracion { get; set; }
        public int TotalValoraciones { get; set; }
        public Ep eps { get; set; }
        public List<EstablecimientoSalud> listEst { get; set; }
        public List<Ep> listEps { get; set; }
        public string Clasificacion { get; set; }
        public List<EstablecimientoSalud> RecEst { get; set; }
    }
}
