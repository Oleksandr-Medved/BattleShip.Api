using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DataAccessLayer.Repositories
{
    public interface IRepository<TContext> where TContext : DbContext
    {
        TContext Context { get; set; }

        Task<T> GetBy<T>(Expression<Func<T, bool>> expression, bool asTracked = true);

        Task<T> GetByWithInclude<T>(Expression<Func<T,bool>> expression, params Expression<Func<T, object>>[] includeProperties);

        Task<IQueryable<T>> Filter<T>(Expression<Func<T, bool>> expression, bool asTracked = true);

        IQueryable<T> GetAll<T>();

        void Add<T>(T entity);

        void Update<T>(T entity);
    }
}
