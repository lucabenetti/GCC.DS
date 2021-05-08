using System;
using System.Linq;

namespace GCC.App.Extensions
{
    public static class StringExtensions
    {
        public static string ApenasNumeros(this string valor) {
            return string.Concat(valor.Where(char.IsDigit));
        }

        public static string FormataCPF(this int valor)
        {
            return valor.ToString(@"000\.000\.000\-00");
        }

        public static string FormataTelefone(this int valor)
        {
            return valor.ToString().Length == 14 ? valor.ToString() : valor.ToString();
        }

    }
}
