using System;
using System.Collections.Generic;
using System.Linq;

namespace MentorsBlog.Core.Common.Extensions
{
    public static class MappersBase
    {
        public static IEnumerable<TResult> MapToList<T, TResult>(this IEnumerable<T> source, Func<T, TResult> func)
        {
            return source?.Select(func.Invoke);
        }
    }
}
