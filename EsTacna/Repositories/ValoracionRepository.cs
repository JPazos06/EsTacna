using EsTacna.Models;
using Microsoft.EntityFrameworkCore;

namespace EsTacna.Repositories
{
    /// <summary>
    /// Interfaz para el repositorio de valoraciones.
    /// </summary>
    public interface IValoracionRepository
    {
        /// <summary>
        /// Guarda una nueva valoración.
        /// </summary>
        /// <param name="valoracion">La valoración a guardar.</param>
        void Guardar(Valoracion valoracion);

        /// <summary>
        /// Lista todas las valoraciones asociadas a un establecimiento por su ID.
        /// </summary>
        /// <param name="establecimientoId">ID del establecimiento.</param>
        /// <returns>Una lista de objetos Valoracion.</returns>
        List<Valoracion> ListarPorEstablecimientoId(int establecimientoId);
    }

    /// <summary>
    /// Implementación del repositorio de valoraciones.
    /// </summary>
    public class ValoracionRepositoryImpl : IValoracionRepository
    {
        private readonly EsTacnaContext _context;

        /// <summary>
        /// Constructor del repositorio de valoraciones.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public ValoracionRepositoryImpl(EsTacnaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Guarda una nueva valoración en la base de datos.
        /// </summary>
        /// <param name="valoracion">La valoración a guardar.</param>
        public void Guardar(Valoracion valoracion)
        {
            try
            {
                _context.Entry(valoracion).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la valoración", ex);
            }
        }

        /// <summary>
        /// Lista todas las valoraciones asociadas a un establecimiento por su ID.
        /// </summary>
        /// <param name="establecimientoId">ID del establecimiento.</param>
        /// <returns>Una lista de objetos Valoracion.</returns>
        public List<Valoracion> ListarPorEstablecimientoId(int establecimientoId)
        {
            try
            {
                return _context.Valoracions.Where(v => v.EstablecimientoId == establecimientoId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las valoraciones por establecimiento ID", ex);
            }
        }
    }
}
