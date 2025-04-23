
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

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
