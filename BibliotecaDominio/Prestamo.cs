using BibliotecaDominio.Helper;
using System;
using System.Linq;

namespace BibliotecaDominio
{
    public class Prestamo
    {
        public DateTime FechaSolicitud { get; }
        public Libro Libro { get; }
        public DateTime? FechaEntregaMaxima { get; }
        public string NombreUsuario { get; }

        public Prestamo(DateTime fechaSolicitud, Libro libro, string nombreUsuario)
        {
            DateTime? fechaMax = null;
            if (IsbnSuma(libro.Isbn) > 30)
            {
                fechaMax = DatetimeExtensions.SumarDiaslaborales(fechaSolicitud, 15);
            }

            this.FechaSolicitud = fechaSolicitud;
            this.Libro = libro;
            this.FechaEntregaMaxima = fechaMax;
            this.NombreUsuario = nombreUsuario;
        }

        private int IsbnSuma(string isbn)
        {
            return isbn.Where(char.IsDigit).Sum(i => (int) char.GetNumericValue(i));
        }
    }
}