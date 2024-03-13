using EsTacna.Models;

namespace EsTacna.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de Entidades Promotoras de Salud (EPS).
    /// </summary>
    public interface EpsRepository
    {

        /// <summary>
        /// Busca una EPS por su ID.
        /// </summary>
        /// <param name="epsId">ID de la EPS a buscar.</param>
        /// <returns>La EPS correspondiente al ID proporcionado.</returns>
        Ep BuscarId(int epsId);

        /// <summary>
        /// Lista todas las EPS almacenadas.
        /// </summary>
        /// <returns>Una lista de objetos EPS.</returns>
        List<Ep> Listar();
    }

    /// <summary>
    /// Implementación del repositorio de Entidades Promotoras de Salud (EPS).
    /// </summary>
    public class EpsRepositoryImpl : EpsRepository
    {
        private readonly EsTacnaContext _dbContext;

        /// <summary>
        /// Constructor de la clase EpRepositoryimpl.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public EpsRepositoryImpl(EsTacnaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public Ep BuscarId(int epsId)
        {
            Ep eps = new Ep();
            try
            {
                var epsDatos = from datos in _dbContext.Eps select datos;
                eps = epsDatos.Where(e => e.Id == epsId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return eps;
        }

        /// <inheritdoc />
        public List<Ep> Listar()
        {
            List<Ep> listEps = new List<Ep>();
            try
            {
                var epsDatos = from datos in _dbContext.Eps select datos;
                listEps = epsDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listEps;
        }
    }
}
