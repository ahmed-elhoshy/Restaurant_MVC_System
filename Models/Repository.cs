
using Microsoft.EntityFrameworkCore;
using Resturant.Data;

namespace Resturant.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        private DbSet<T> _dbSet { get; set; }

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id, new QueryOptions<T>());
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;

            // Apply includes
            foreach (var include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            // Apply where clause
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }

            // Use reflection to find the primary key property dynamically
            var keyProperty = _context.Model.FindEntityType(typeof(T))
                .FindPrimaryKey()
                .Properties
                .FirstOrDefault();

            if (keyProperty == null)
            {
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have a primary key defined.");
            }

            var keyName = keyProperty.Name;

            // Filter by the primary key
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
