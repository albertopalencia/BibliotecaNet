using BibliotecaRepositorio.Entidades;

namespace BibliotecaRepositorio.Repositorio
{
    public interface IRepositorioLibroEF
    {
        /// <summary>
        /// Permite obtener un libro entity por un isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        LibroEntidad ObtenerLibroEntidadPorIsbn(string isbn);
    }
}