using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Extensions
{
    public static class IEnumerableExtensions
    {
        public static Task LoopAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
            return Task.WhenAll(list.Select(func));
        }
    }
}
