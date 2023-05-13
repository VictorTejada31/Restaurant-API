using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class DishCategoryRepository : GenericRepository<DishCategory>, IDishCategoryRepository
    {
        private readonly ApplicationContext _context;
        public DishCategoryRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
