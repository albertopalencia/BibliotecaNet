using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDominio.Helper
{
    public static class DatetimeExtensions
    {
        public static DateTime SumarDiaslaborales(DateTime actual, int dias)
        {
            DateTime diaAgregado = actual;
            var agrega = Math.Sign(dias);
            var diasPositivos = Math.Abs(dias);

            for (var i = 0; i < diasPositivos; i++)
            {
                do
                {
                    diaAgregado = diaAgregado.AddDays(agrega);
                } while (diaAgregado.DayOfWeek == DayOfWeek.Sunday);
            }

            if (diaAgregado.DayOfWeek == DayOfWeek.Sunday) diaAgregado.AddDays(1);

            return diaAgregado;
        }
    }
}
