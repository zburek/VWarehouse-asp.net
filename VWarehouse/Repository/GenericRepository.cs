using Model.Common.DbEntities;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly DbContext context;
        public GenericRepository(DbContext context)
        {
            this.context = context;
        }
        protected virtual IQueryable<TEntity> GetQueryable
            (Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {

            includeProperties = includeProperties ?? string.Empty;
            
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }

        #region Get
        public virtual async Task<TEntity> GetByIdAsync(int? ID)
        {
            return await context.Set<TEntity>().FindAsync(ID);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }

        #endregion

        #region CRUD
        public virtual Task<int> AddAsync(TEntity entity)
        {
            try
            {
                var dbEntityEntry = context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    context.Set<TEntity>().Add(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                var dbEntityEntry = context.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    context.Set<TEntity>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual Task<int> DeleteAsync(object ID)
        {
            var entity = context.Set<TEntity>().Find(ID);
            if (entity == null)
            {
                return Task.FromResult(0);
            }
            return DeleteAsync(entity);
        }

        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            try
            {
                var dbEntityEntry = context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    context.Set<TEntity>().Attach(entity);
                    context.Set<TEntity>().Remove(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
