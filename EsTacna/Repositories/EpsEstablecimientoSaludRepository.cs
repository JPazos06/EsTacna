using EsTacna.Models;
using Microsoft.EntityFrameworkCore;

namespace EsTacna.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de relación entre EPS y Establecimientos de Salud.
    /// </summary>
    public interface EpsEstablecimientoSaludRepository
    {
        /// <summary>
        /// Busca una relación entre EPS y Establecimiento de Salud por ID de establecimiento.
        /// </summary>
        /// <param name="establecimientoId">ID del establecimiento de salud a buscar.</param>
        /// <returns>La relación EPS-Establecimiento correspondiente al ID proporcionado.</returns>
        EpsEstablecimientoSalud BuscarId(int establecimientoId);

        /// <summary>
        /// Busca una EPS asociada a un Establecimiento de Salud por ID de establecimiento.
        /// </summary>
        /// <param name="establecimientoId">ID del establecimiento de salud para el que se busca la EPS asociada.</param>
        /// <returns>La EPS asociada al establecimiento de salud.</returns>
        Ep BuscarIdEps(int establecimientoId);
    }

    /// <summary>
    /// Implementación del repositorio de relación entre EPS y Establecimientos de Salud.
    /// </summary>
    public class EpsEstablecimientoSaludRepositoryImpl : EpsEstablecimientoSaludRepository
    {
        private readonly EsTacnaContext _dbContext;

        /// <summary>
        /// Constructor de la clase EpsEstablecimientoSaludRepositoryImpl.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public EpsEstablecimientoSaludRepositoryImpl(EsTacnaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public EpsEstablecimientoSalud BuscarId(int establecimientoId)
        {
            EpsEstablecimientoSalud objEpsEstablecimiento = new EpsEstablecimientoSalud();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EpsEstablecimientoSaluds select datos;
                objEpsEstablecimiento = establecimientoDatos.Where(e => e.EstablecimientoId == establecimientoId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return objEpsEstablecimiento;
        }

        /// <inheritdoc />
        public Ep BuscarIdEps(int establecimientoId)
        {
            Ep objEps = new Ep();
            try
            {
                objEps = _dbContext.EpsEstablecimientoSaluds
                        .Include(e => e.Eps)
                        .FirstOrDefault(e => e.EstablecimientoId == establecimientoId)?.Eps;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return objEps;
        }
    }
}
