using BibliotecaDominio.IRepositorio;
using System;

namespace BibliotecaDominio
{
    public class Bibliotecario
    {
        public const string ElLibroNoSeEncuentraDisponible = "El libro no se encuentra disponible";
        private readonly IRepositorioLibro libroRepositorio;
        private readonly IRepositorioPrestamo prestamoRepositorio;

        public Bibliotecario(IRepositorioLibro libroRepositorio, IRepositorioPrestamo prestamoRepositorio)
        {
            this.libroRepositorio = libroRepositorio;
            this.prestamoRepositorio = prestamoRepositorio;
        }

        public void Prestar(string isbn, string nombreUsuario)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                throw new ArgumentException("ISBN no puede ser null");
            }

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new ArgumentException("El nombre del usuario no puede ser null");
            }

            if (EsPrestado(isbn))
            {
                throw new Exception(ElLibroNoSeEncuentraDisponible);
            }

            var libroaprestar = libroRepositorio.ObtenerPorIsbn(isbn);

            if (libroaprestar == null) return;
            var diaPrestamo = DateTime.Now;
            var enPrestamo = new Prestamo(diaPrestamo, libroaprestar, nombreUsuario);
            prestamoRepositorio.Agregar(enPrestamo);
        }

        public bool EsPrestado(string isbn)
        {
            return prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn) != null;
        }
    }
}