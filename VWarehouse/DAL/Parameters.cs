using DAL.DbEntities;
using System;
using System.Linq;
using System.Linq.Expressions;


namespace DAL
{
    public class Parameters<TEntity> : IParameters<TEntity> where TEntity : class, IBaseEntity
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public string IncludeProperties { get; set; }
        public Expression<Func<TEntity, bool>> Filter { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
    }
}
