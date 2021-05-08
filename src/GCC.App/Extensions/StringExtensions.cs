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
            return Convert.ToInt64(valor).ToString(@"000\.000\.000\-00");
        }

        public static string FormataTelefone(this string valor)
        {
            valor = valor.ApenasNumeros();
            var valorNumerico = Convert.ToInt64(valor);
            return valor.Length == 10 ? valorNumerico.ToString(@"(00) 0000-0000") : valorNumerico.ToString(@"(00) 00000-0000");
        }

    }
}
