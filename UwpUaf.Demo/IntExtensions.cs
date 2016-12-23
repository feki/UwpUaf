using System.Collections.Generic;

namespace UwpUaf.Demo
{
    internal static class IntExtensions
    {
        public static IEnumerable<int> Times(this int times)
        {
            for (int i = 0; i < times; i++)
            {
                yield return i;
            }
        }
    }
}
