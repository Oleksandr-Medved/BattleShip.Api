using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BattleShip.DataAccessLayer.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;

        private readonly DbSet<TEntity> dbSet;

        private readonly ILogger<GenericRepository<TEntity>> logger;

        public GenericRepository(DbContext context, ILogger<GenericRepository<TEntity>> logger)
        {
            this.dbContext = context;
            this.dbSet = dbContext.Set<TEntity>();
            this.logger = logger;
        }

        public void Add(TEntity entity)
        {
            this.logger.LogInformation($"Add new entity = {typeof(TEntity)}");
            this.dbSet.Add(entity);
            this.dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this.logger.LogInformation($"Delete entity = {typeof(TEntity)}");
            this.dbSet.Remove(entity);
            this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            this.logger.LogInformation($"Get all entities = {typeof(TEntity)}");
            return await this.dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetBy(Expression<Func<TEntity, bool>> expression,
            bool asTracked = true)
        {
            this.logger.LogInformation($"Get entities = {typeof(TEntity)} by");

            if (asTracked)
            {
                return await this.dbSet.AsTracking().Where(expression).ToListAsync();
            }

            return await this.dbSet.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByWithInclude(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = includeProperties
         .Aggregate(this.dbSet.AsNoTracking(), (current, includeProperty) =>
         current.Include(includeProperty))
         .AsQueryable();

            return await query.Where(expression).ToListAsync();
        }

        public async Task<TEntity> GetEntityBy(Expression<Func<TEntity, bool>> expression)
        {
            this.logger.LogInformation($"Get first entity = {typeof(TEntity)}");
            return await this.dbSet.FirstAsync(expression);
        }

        public void Update(TEntity entity)
        {
            this.logger.LogInformation($"Update entities = {typeof(TEntity)}");
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }
    }
}
