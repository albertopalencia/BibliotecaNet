namespace BibliotecaDominio.IRepositorio
{
    public interface IRepositorioLibro
    {
        
        Libro ObtenerPorIsbn(string isbn);
        void Agregar(Libro libro);
    }
}