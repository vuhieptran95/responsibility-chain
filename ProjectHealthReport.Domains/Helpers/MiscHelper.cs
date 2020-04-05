using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectHealthReport.Domains.Helpers
{
    public static partial class MiscHelper
    {
        // Ex: collection.TakeLast(5);
        public static IEnumerable<T> TakeLast<T>(this IList<T> source, int last)
        {
            return source.Skip(Math.Max(0, source.Count - last));
        }

        public static decimal RoundDown(double x, double decimalPlaces = 1)
        {
            var i = Convert.ToDecimal(x);
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(i * power) / power;
        }
    }
}
