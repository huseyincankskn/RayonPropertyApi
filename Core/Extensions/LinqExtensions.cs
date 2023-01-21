using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Extensions
{
    public static class LinqExtensions
    {
        public static Expression<Func<T, bool>> CombineWith<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var body = Expression.AndAlso(expression1.Body, expression2.Body);
            return Expression.Lambda<Func<T, bool>>(body, expression1.Parameters[0]);
        }

        public static IQueryable<T> WhereEquals<T>(this IQueryable<T> query, string propertyName, object propertyValue) where T : class, new()
        {
            var entityToComparison = Expression.Parameter(typeof(T));
            var entityPropertyToComparison = Expression.PropertyOrField(entityToComparison, propertyName);
            var valueToComparison = Expression.Constant(Convert.ChangeType(propertyValue, typeof(T).GetProperty(propertyName).PropertyType));

            // x.{propertyName} == value
            var whereBody = Expression.Equal(entityPropertyToComparison, valueToComparison);
            // x => x.{propertyName} == value
            var whereLambda = Expression.Lambda<Func<T, bool>>(whereBody, entityToComparison);

            // Queryable.Where(query, x => x.{propertyName} == value)
            var whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { typeof(T) },
                query.Expression,
                whereLambda
            );

            // query.Where(x => x.{propertyName} == value)
            return query.Provider.CreateQuery<T>(whereCallExpression);
        }
    }
}