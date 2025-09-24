using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfNames
{
    public static class ListExtensions
    {
        public static List<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var rng = new Random();
            return source.OrderBy(_ => rng.Next()).ToList();
        }
    }
}
