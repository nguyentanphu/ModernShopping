using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ModernShopping.Application.Utils.Mappers
{
    public static class MapperHelper
    {
        public static TDest Map<TSource, TDest>(this TSource input, Func<TSource, TDest> func)
            where TSource : class
            where TDest : class
        {
            return func(input);
        }
    }
}
