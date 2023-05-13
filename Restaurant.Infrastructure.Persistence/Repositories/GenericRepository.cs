using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Entity> AddAsync(Entity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<Entity> UpdateAsync(Entity entity, int id)
        {
            var entry = await _context.Set<Entity>().FindAsync(id);
            _context.Entry<Entity>(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task DeleteAsync(int id)
        {
            Entity entity = await _context.Set<Entity>().FindAsync(id);
             _context.Set<Entity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>().ToListAsync();
        }
        public async Task<List<Entity>> GetAllWithIncludesAsync(List<string> properties)
        {
            var entities =  _context.Set<Entity>().AsQueryable();
            foreach (var property in properties)
            {
                entities = entities.Include(property);
            }

            return await entities.ToListAsync();
        }
    }
}
