using EsTacna.Models;
using Microsoft.EntityFrameworkCore;

namespace EsTacna.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de Establecimientos de Salud.
    /// </summary>
    public interface EstablecimientoSaludRepository
    {
        List<EstablecimientoSalud> Buscar(string criterio, int epsId);
        EstablecimientoSalud BuscarId(int establecimientoId);
        List<EstablecimientoSalud> Listar(int epsId);
        List<EstablecimientoSalud> ListarMap();
    }

    /// <summary>
    /// Implementación del repositorio de Establecimientos de Salud.
    /// </summary>
    public class EstablecimientoSaludRepositoryImpl : EstablecimientoSaludRepository
    {
        private readonly EsTacnaContext _dbContext;

        public EstablecimientoSaludRepositoryImpl(EsTacnaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public List<EstablecimientoSalud> Buscar(string criterio, int epsId)
        {
            List<EstablecimientoSalud> listEstablecimiento = new List<EstablecimientoSalud>();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EstablecimientoSaluds
                                           join epsEst in _dbContext.EpsEstablecimientoSaluds on datos.Id equals epsEst.EstablecimientoId
                                           where epsEst.EpsId == epsId &&
                                                 (datos.Nombre.ToLower().Contains(criterio.ToLower()) || datos.Descripcion.ToLower().Contains(criterio.ToLower()))
                                           select datos;

                listEstablecimiento = establecimientoDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listEstablecimiento;
        }

        /// <inheritdoc />
        public EstablecimientoSalud BuscarId(int estId)
        {
            EstablecimientoSalud establecimientoDeSalud = new EstablecimientoSalud();
            try
            {
                establecimientoDeSalud = _dbContext.EstablecimientoSaluds
                    .Include(e => e.EpsEstablecimientoSaluds)
                    .FirstOrDefault(e => e.Id == estId);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }

            return establecimientoDeSalud;
        }

        /// <inheritdoc />
        public List<EstablecimientoSalud> Listar(int epsId)
        {
            List<EstablecimientoSalud> listEstablecimiento = new List<EstablecimientoSalud>();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EstablecimientoSaluds
                                           join epsEst in _dbContext.EpsEstablecimientoSaluds on datos.Id equals epsEst.EstablecimientoId
                                           where epsEst.EpsId == epsId
                                           select datos;

                listEstablecimiento = establecimientoDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listEstablecimiento;
        }

        /// <inheritdoc />
        public List<EstablecimientoSalud> ListarMap()
        {
            List<EstablecimientoSalud> listEstablecimiento = new List<EstablecimientoSalud>();
            try
            {
                var establecimientoDatos = from datos in _dbContext.EstablecimientoSaluds select datos;
                listEstablecimiento = establecimientoDatos.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw;
            }
            return listEstablecimiento;
        }
    }

    /// <summary>
    /// Define la interfaz para la unidad de trabajo de Establecimientos de Salud.
    /// </summary>
    public interface IUnitOfWorkEst : IDisposable
    {
        EstablecimientoSaludRepository EstablecimientoSaludRepository { get; }
        void SaveChanges();
    }

    /// <summary>
    /// Implementación de la unidad de trabajo de Establecimientos de Salud.
    /// </summary>
    public class UnitOfWorkEst : IUnitOfWorkEst
    {
        private readonly EsTacnaContext _dbContext;
        private EstablecimientoSaludRepository _establecimientosaludrepository;

        /// <summary>
        /// Constructor de la clase UnitOfWorkEst.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public UnitOfWorkEst(EsTacnaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public EstablecimientoSaludRepository EstablecimientoSaludRepository
        {
            get
            {
                if (_establecimientosaludrepository == null)
                {
                    _establecimientosaludrepository = new EstablecimientoSaludRepositoryImpl(_dbContext);
                }
                return _establecimientosaludrepository;
            }
        }

        /// <inheritdoc />
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
