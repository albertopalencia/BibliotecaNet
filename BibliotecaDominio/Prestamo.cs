using BibliotecaDominio.Helper;
using System;

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
            // regla 5 - los dígitos numericos que componen el ISBN suman más de 30...
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
            int suma = 0;
            foreach (char i in isbn)
            {
                if (char.IsDigit(i))
                {
                    suma += (int)char.GetNumericValue(i);
                }
            }

            return suma;
        }
    }
}