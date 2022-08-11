using Microsoft.EntityFrameworkCore;

namespace GhostOfTrebec.Core.InnerCore
{
    public class DbContextRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        private readonly TContext _context;

        public DbContextRepository(TContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TEntity>> FindAsync(Specification<TEntity> specification)
        {
            var results = await _context.Set<TEntity>()
                .AsTracking()
                .Where(specification.ToExpression())
                .ToListAsync();

            return results;
        }

        public async Task SaveAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
