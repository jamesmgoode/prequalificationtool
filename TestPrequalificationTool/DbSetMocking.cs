using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Language;
using Moq.Language.Flow;

namespace TestPrequalificationTool
{
    public static class DbSetMocking
    {
        private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> queryable)
            where T : class
        {
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return mockDbSet;
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
            this IReturns<TContext, DbSet<TEntity>> setup,
            TEntity[] entities)
            where TEntity : class
            where TContext : DbContext
        {
            var mockDbSet = CreateMockDbSet(entities.AsQueryable());
            return setup.Returns(mockDbSet.Object);
        }
    }
}
