using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ModernShopping.Application.Utils.Queryable
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> ApplyWhere<TEntity>(this IQueryable<TEntity> query, bool apply,
            Expression<Func<TEntity, bool>> predicate)
        {
            if (apply) return query.Where(predicate);
            return query;
        }
    }
}
