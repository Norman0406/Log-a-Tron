using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Logatron.Database.Util
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(
            this IQueryable<TEntity> source,
            string orderByProperty,
            bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            Type type = typeof(TEntity);
            PropertyInfo? property = type.GetProperty(orderByProperty) ?? throw new InvalidOperationException("Invalid property");
            ParameterExpression parameter = Expression.Parameter(
                    type,
                    "p");
            MemberExpression propertyAccess = Expression.MakeMemberAccess(
                    parameter,
                    property);
            LambdaExpression orderByExpression = Expression.Lambda(
                    propertyAccess,
                    parameter);
            MethodCallExpression resultExpression = Expression.Call(
                    typeof(Queryable),
                    command,
                    new Type[] { type, property.PropertyType },
                    source.Expression, Expression.Quote(orderByExpression));
            return (IOrderedQueryable<TEntity>)source
                    .Provider
                    .CreateQuery<TEntity>(resultExpression);
        }
    }
}
