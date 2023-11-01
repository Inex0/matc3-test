using System.Collections.Generic;
using Core.Utilities;

namespace Core.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            for (var i = list.Count; i > 0; i--)
            {
                list.Swap(0, RandomUtil.Range(0, i));
            }
        }

        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }

            return list[RandomUtil.Range(list.Count)];
        }

        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
