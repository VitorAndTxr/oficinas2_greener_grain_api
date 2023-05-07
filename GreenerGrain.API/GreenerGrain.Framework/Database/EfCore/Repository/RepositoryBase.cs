using API.Framework.Interfaces;
using GreenerGrain.Framework.Database.EfCore.Factory;
using GreenerGrain.Framework.Database.EfCore.Interface;
using GreenerGrain.Framework.Database.EfCore.Model;
using GreenerGrain.Framework.Database.EfCore.PagedResult;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GreenerGrain.Framework.Database.EfCore.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        private readonly IDbFactory _dbFactory;
        private readonly IApiContext _apiContext;

        private DbSet<TEntity> _dbSet;
        protected DbSet<TEntity> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<TEntity>());            
        }

        public RepositoryBase(
            IDbFactory dbFactory
            , IApiContext apiContext)
        {
            _dbFactory = dbFactory;
            this._apiContext = apiContext;
        }

        public PagedList<TEntity> GetPagination(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = "")
        {   
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return PagedList<TEntity>.ToPagedList(query, _apiContext);            
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , string includeProperties = "")       
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includeProperties)
            {
                MemberExpression memberExpression = include.Body as MemberExpression;

                if (memberExpression != null)
                    query = query.Include(memberExpression.Member.Name);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(object id, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var include in includes)
            {
                MemberExpression memberExpression = include.Body as MemberExpression;

                if (memberExpression != null)
                    DbSet.Include(memberExpression.Member.Name);
            }

            return await DbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return DbSet
                .FromSqlRaw(query, parameters)
                .ToList();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            _dbFactory.DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IList<TEntity> entities)
        {
            DbSet.AttachRange(entities);
        }

        public void Delete(TEntity entity)
        {
            if (_dbFactory.DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_dbFactory.DbContext.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }

            DbSet.RemoveRange(entities);
        }

        #region Dispose

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbFactory.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
