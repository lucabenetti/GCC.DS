using System;

namespace GCC.App.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool MesmoDia(this DateTime data, DateTime dataAComparar)
        {
            return Equals(data.Year, dataAComparar.Year) && Equals(data.Month, dataAComparar.Month) && Equals(data.Day, dataAComparar.Day);
        }
    }
}
