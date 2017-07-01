using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common
{
    public interface IParameters<TEntity> where TEntity : class, IBaseEntity
    {
        int? PageSize { get; set; }
        int? PageNumber { get; set; }
        int? Skip { get; set; }
        int? Take { get; set; }
        string SearchString { get; set; }
        string SortOrder { get; set; }
        string IncludeProperties { get; set; }
        Expression<Func<TEntity, bool>> Filter { get; set; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
    }
}
