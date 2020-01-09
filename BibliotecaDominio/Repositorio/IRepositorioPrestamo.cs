namespace BibliotecaDominio.IRepositorio
{
    public interface IRepositorioPrestamo
    {
       
        Libro ObtenerLibroPrestadoPorIsbn(string isbn);

        void Agregar(Prestamo prestamo);

       
        Prestamo Obtener(string isbn);
    }
}