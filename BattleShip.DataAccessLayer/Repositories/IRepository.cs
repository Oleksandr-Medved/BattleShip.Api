using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BattleShip.DataAccessLayer.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetBy(Expression<Func<TEntity, bool>> expression, bool asTracked = true);

        Task<TEntity> GetEntityBy(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetByWithInclude(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetAll();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
