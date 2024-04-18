using EsTacna.Models;
using Microsoft.EntityFrameworkCore;

namespace EsTacna.Repositories
{
    /// <summary>
    /// Define la interfaz para el repositorio de usuarios.
    /// </summary>
    public interface UsuarioRepository
    {
        /// <summary>
        /// Registra un usuario en la base de datos.
        /// </summary>
        /// <param name="objUsuario">Objeto Usuario que se registrará.</param>
        void Registrar(Usuario objUsuario);

        /// <summary>
        /// Realiza el inicio de sesión de un usuario.
        /// </summary>
        /// <param name="usuarioCuenta">Nombre de usuario o correo electrónico.</param>
        /// <param name="contrasenaCuenta">Contraseña del usuario.</param>
        /// <returns>El objeto Usuario correspondiente al inicio de sesión.</returns>
        Usuario Login(string usuarioCuenta, string contrasenaCuenta);

        /// <summary>
        /// Busca un usuario por su ID.
        /// </summary>
        /// <param name="usuarioId">ID del usuario a buscar.</param>
        /// <returns>El objeto Usuario correspondiente al ID proporcionado.</returns>
        Usuario BuscarId(int usuarioId);
    }

    /// <summary>
    /// Implementación del repositorio de usuarios.
    /// </summary>
    public class UsuarioRepositoryImpl : UsuarioRepository
    {
        private readonly EsTacnaContext _dbContext;

        public UsuarioRepositoryImpl(EsTacnaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public void Registrar(Usuario objUsuario)
        {
            try
            {
                if (objUsuario.Id > 0)
                {
                    _dbContext.Entry(objUsuario).State = EntityState.Modified;
                }
                else
                {
                    _dbContext.Entry(objUsuario).State = EntityState.Added;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al registrar el usuario.", ex);
            }
        }

        /// <inheritdoc />
        public Usuario Login(string usuarioCuenta, string contrasenaCuenta)
        {
            Usuario objUsuario = new Usuario();
            try
            {
                var usuarioDatos = from datos in _dbContext.Usuarios select datos;
                objUsuario = usuarioDatos.Where(u => u.Email == usuarioCuenta && u.Contrasena == contrasenaCuenta).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("El usuario y/o la contraseña son incorrectos", ex);
            }
            return objUsuario;
        }

        /// <inheritdoc />
        public Usuario BuscarId(int usuarioId)
        {
            Usuario objUsuario = new Usuario();
            try
            {
                var usuarioDatos = from datos in _dbContext.Usuarios select datos;
                objUsuario = usuarioDatos.Where(e => e.Id == usuarioId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return objUsuario;
        }
    }

    /// <summary>
    /// Define la interfaz para la unidad de trabajo.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        UsuarioRepository UsuarioRepositoryimpl { get; }
        void SaveChanges();
    }

    /// <summary>
    /// Implementación de la unidad de trabajo.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EsTacnaContext _dbContext;
        private UsuarioRepository _usuarioRepository;

        /// <summary>
        /// Constructor de la clase UnitOfWork.
        /// </summary>
        /// <param name="dbContext">Contexto de base de datos para acceder a las entidades.</param>
        public UnitOfWork(EsTacnaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public UsuarioRepository UsuarioRepositoryimpl
        {
            get
            {
                if (_usuarioRepository == null)
                {
                    _usuarioRepository = new UsuarioRepositoryImpl(_dbContext);
                }
                return _usuarioRepository;
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
