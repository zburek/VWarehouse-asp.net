using Common;
using Common.Parameters.RepositoryParameters;
using DAL;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{   
    public class GenericRepository : IGenericRepository
    {
        protected readonly IVWarehouseContext Context;
        internal IUnitOfWorkFactory UnitOfWorkFactory;
        public GenericRepository(IVWarehouseContext context, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.Context = context;
            this.UnitOfWorkFactory = unitOfWorkFactory;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return UnitOfWorkFactory.CreateUnitOfWork(Context);
        }
        private IQueryable<TEntity> GetQueryable<TEntity>(IGenericRepositoryParameters<TEntity> parameters) where TEntity : class, IBaseEntity
        {

            parameters.IncludeProperties = parameters.IncludeProperties ?? string.Empty;

            IQueryable<TEntity> query = Context.Set<TEntity>();

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
        public virtual async Task<TEntity> GetByIdAsync<TEntity>(Guid? ID) where TEntity : class, IBaseEntity
        {
            return await Context.Set<TEntity>().FindAsync(ID);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(IGenericRepositoryParameters<TEntity> parameters) where TEntity : class, IBaseEntity
        {
            return await GetQueryable<TEntity>(parameters).ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(IGenericRepositoryParameters<TEntity> parameters) where TEntity : class, IBaseEntity
        {
            return await GetQueryable(parameters).SingleOrDefaultAsync();
        }

        public virtual Task<int> GetCountAsync<TEntity>(IGenericRepositoryParameters<TEntity> parameters) where TEntity : class, IBaseEntity
        {
            return GetQueryable(parameters).CountAsync();
        }

        #endregion

        #region CRUD
        public virtual async Task<int> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            try
            {
                var dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    Context.Set<TEntity>().Add(entity);
                    await Context.SaveChangesAsync();
                }
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual async Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            try
            {
                var dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    Context.Set<TEntity>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> DeleteAsync<TEntity>(Guid ID) where TEntity : class, IBaseEntity
        {
            var entity = Context.Set<TEntity>().Find(ID);
            if (entity == null)
            {
                return await Task.FromResult(0);
            }
            return await DeleteAsync(entity);
        }

        private async Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            try
            {
                var dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    Context.Set<TEntity>().Attach(entity);
                    Context.Set<TEntity>().Remove(entity);
                }
                await Context.SaveChangesAsync();
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}