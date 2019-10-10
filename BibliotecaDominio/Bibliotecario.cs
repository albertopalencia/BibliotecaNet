using BibliotecaDominio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio
{
    public class Bibliotecario
    {
        public const string EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE = "El libro no se encuentra disponible";
        private  IRepositorioLibro libroRepositorio;
        private  IRepositorioPrestamo prestamoRepositorio;

        public Bibliotecario(IRepositorioLibro libroRepositorio, IRepositorioPrestamo prestamoRepositorio)
        {
            this.libroRepositorio = libroRepositorio;
            this.prestamoRepositorio = prestamoRepositorio;
        }

        public void Prestar(string isbn, string nombreUsuario)
        {
            // regla de negocio 1 - debe entregar el ISBN
            if (string.IsNullOrEmpty(isbn))
            {
                throw new ArgumentException("ISBN no puede ser null");
            }
            // regla de negocio 4 - debe solicitar el nombreUsuario
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new ArgumentException("El nombre del usuario no puede ser null");
            }
            // regla 2 - un isbn no se puede prestar más de una vez
            if(EsPrestado(isbn))
            {
                Exception ex = new Exception(EL_LIBRO_NO_SE_ENCUENTRA_DISPONIBLE);
                throw ex;
            }
            // que el libro exista en la biblioteca
            var libroaprestar = libroRepositorio.ObtenerPorIsbn(isbn);
            // si existe y no esta prestado valide y prestarlo
            if (libroaprestar != null)
            {
                DateTime diaPrestamo = DateTime.Now;

                Prestamo enPrestamo = new Prestamo(diaPrestamo, libroaprestar, nombreUsuario);

                prestamoRepositorio.Agregar(enPrestamo);

            }
        }


        public bool EsPrestado(string isbn)
        {
            var prestado = prestamoRepositorio.ObtenerLibroPrestadoPorIsbn(isbn);

            return prestado != null;
        }
    }
}
