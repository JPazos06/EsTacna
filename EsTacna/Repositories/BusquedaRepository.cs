using EsTacna.Models;
using Microsoft.EntityFrameworkCore;

namespace EsTacna.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de búsquedas.
    /// </summary>
    public interface BusquedaRepository
    {
        /// <summary>
        /// Registra una búsqueda en la base de datos.
        /// </summary>
        /// <param name="busqueda">Objeto Busquedum que se registrará.</param>
        void Registrar(Busquedum busqueda);

        /// <summary>
        /// Lista todas las búsquedas almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de objetos Busquedum.</returns>
        List<Busquedum> ListarBusqueda();
    }

    /// <summary>
    /// Implementación del repositorio de búsquedas.
    /// </summary>
    public class BusquedaRepositoryImpl : BusquedaRepository
    {
        private readonly EsTacnaContext _dbContext;

        /// <summary>
        /// Constructor de la clase BusquedaRepositoryimpl.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public BusquedaRepositoryImpl(EsTacnaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public void Registrar(Busquedum busqueda)
        {
            try
            {
                _dbContext.Entry(busqueda).State = EntityState.Added;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public List<Busquedum> ListarBusqueda()
        {
            List<Busquedum> listBusqueda = new List<Busquedum>();
            try
            {
                var busquedaDatos = from datos in _dbContext.Busqueda select datos;
                listBusqueda = busquedaDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listBusqueda;
        }
    }
}
