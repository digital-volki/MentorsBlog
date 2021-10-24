using System;
using System.Collections.Generic;

namespace MentorsBlog.Core.Common.Extensions
{
    public static class MappersBase
    {
        public static IEnumerable<TResult> MapToList<T, TResult>(this IEnumerable<T> source, Func<T, TResult> func)
        {
            if (source == null)
            {
                return new List<TResult>();
            }

            var result = new List<TResult>();

            foreach (var item in source)
            {
                if (item == null)
                {
                    continue;
                }

                result.Add(func.Invoke(item));
            }

            return result;
        }
    }
}
