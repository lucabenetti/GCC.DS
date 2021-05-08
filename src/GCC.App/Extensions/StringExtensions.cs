using System;
using System.Linq;

namespace GCC.App.Extensions
{
    public static class StringExtensions
    {
        public static string ApenasNumeros(this string valor) {
            return string.Concat(valor.Where(char.IsDigit));
        }

        public static string FormataCPF(this string valor)
        {
            valor = valor.ApenasNumeros();
            return Convert.ToInt32(valor).ToString(@"000\.000\.000\-00");
        }

        public static string FormataTelefone(this string valor)
        {
            valor = valor.ApenasNumeros();
            return Convert.ToInt32(valor).ToString().Length == 14 ? valor.ToString() : valor.ToString();
        }

    }
}
