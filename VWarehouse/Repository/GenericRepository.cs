using Model.Common.DbEntities;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
            string includeProperties = null)
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
            return await GetQueryable(filter, orderBy, includeProperties).ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }
        #endregion

        #region CRUD
        public virtual async Task AddAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                await Task.Run(() => context.Set<TEntity>().Add(entity));
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }

        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                await Task.Run(() => context.Set<TEntity>().Attach(entity));

                await Task.Run(() => context.Entry(entity).State = EntityState.Modified);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        
        public virtual async Task DeleteAsync(object ID)
        {
            TEntity entity = context.Set<TEntity>().Find(ID);
            await DeleteAsync(entity);
        }
        
        public virtual async Task DeleteAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                var dbSet = context.Set<TEntity>();
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    await Task.Run(() => dbSet.Attach(entity));
                }
                await Task.Run(() => dbSet.Remove(entity));
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        #endregion
    }
}
