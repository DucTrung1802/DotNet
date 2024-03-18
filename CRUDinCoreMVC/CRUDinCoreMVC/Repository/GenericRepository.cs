using CRUDinCoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDinCoreMVC.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EFCoreDbContext _context;

        private readonly DbSet<T> _dbSet;

        public GenericRepository(EFCoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task InsertAsync(T Entity)
        {
            await _dbSet.AddAsync(Entity);
        }

        public async Task UpdateAsync(T Entity)
        {
            _dbSet.Update(Entity);
        }

        public async Task DeleteAsync(object Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
