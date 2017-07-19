using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Parameters.RepositoryParameters
{
    public interface IGenericRepositoryParameters<TEntity> : IParameters
    {
        Expression<Func<TEntity, bool>> Filter { get; set; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
    }
    public class GenericRepositoryParameters<TEntity> : Parameters, IGenericRepositoryParameters<TEntity>
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
    }
}
