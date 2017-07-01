using Common;
using DAL;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly DbContext context;
        public GenericRepository(VWarehouseContext context)
        {
            this.context = context;
        }
        protected virtual IQueryable<TEntity> GetQueryable(IParameters<TEntity> parameters)
        {

            parameters.IncludeProperties = parameters.IncludeProperties ?? string.Empty;

            IQueryable<TEntity> query = context.Set<TEntity>();

            if (parameters.Filter != null)
            {
                query = query.Where(parameters.Filter);
            }

            foreach (var includeProperty in parameters.IncludeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (parameters.OrderBy != null)
            {
                query = parameters.OrderBy(query);
            }
            if (parameters.Skip.HasValue)
            {
                query = query.Skip(parameters.Skip.Value);
            }
            if (parameters.Take.HasValue)
            {
                query = query.Take(parameters.Take.Value);
            }
            return query;
        }

        #region Get
        public virtual async Task<TEntity> GetByIdAsync(Guid? ID)
        {
            return await context.Set<TEntity>().FindAsync(ID);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(IParameters<TEntity> parameters)
        {
            return await GetQueryable(parameters).ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync(IParameters<TEntity> parameters)
        {
            return await GetQueryable(parameters).SingleOrDefaultAsync();
        }

        public virtual Task<int> GetCountAsync(IParameters<TEntity> parameters)
        {
            IParameters<TEntity> CountParameters = new Parameters<TEntity>();
            CountParameters.Filter = parameters.Filter;
            return GetQueryable(CountParameters).CountAsync();
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

        public virtual Task<int> DeleteAsync(Guid ID)
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

        #region Save/Dispose

        public virtual async Task<int> SaveAsync()
        {
            int result = 0;
            try
            {
                result = await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        #endregion
    }
}