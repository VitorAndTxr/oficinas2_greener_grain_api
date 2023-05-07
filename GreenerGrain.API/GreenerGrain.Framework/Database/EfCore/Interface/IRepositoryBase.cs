using GreenerGrain.Framework.Database.EfCore.Model;
using GreenerGrain.Framework.Database.EfCore.PagedResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GreenerGrain.Framework.Database.EfCore.Interface
{
    public interface IRepositoryBase<TEntity> 
        where TEntity : class
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IList<TEntity> entiies);

        PagedList<TEntity> GetPagination(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetByIdAsync(object id);

        Task<TEntity> GetByIdAsync(object id, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

        void Update(TEntity entity);

        void UpdateRange(IList<TEntity> entiies);
    }
}
