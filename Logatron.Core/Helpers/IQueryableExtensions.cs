using System.Linq.Expressions;

namespace Logatron.Core.Helpers;

public static class IQueryableExtensions
{
    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(
        this IQueryable<TEntity> source,
        string orderByProperty,
        bool desc)
    {
        var command = desc ? "OrderByDescending" : "OrderBy";
        var type = typeof(TEntity);
        var property = type.GetProperty(orderByProperty) ?? throw new InvalidOperationException("Invalid property");
        var parameter = Expression.Parameter(
                type,
                "p");
        var propertyAccess = Expression.MakeMemberAccess(
                parameter,
                property);
        var orderByExpression = Expression.Lambda(
                propertyAccess,
                parameter);
        var resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
        return (IOrderedQueryable<TEntity>)source
                .Provider
                .CreateQuery<TEntity>(resultExpression);
    }
}
