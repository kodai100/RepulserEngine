using System.Collections.Generic;
using System.Linq;

namespace ProjectBlue.RepulserEngine.Infrastructure
{
    public static class IEnumarableExtension
    {
        public static IEnumerable<(T, int)> WithIndex<T>(this IEnumerable<T> ts)
        {
            return ts.Select((t, i) => (t, i));
        }
    }

}