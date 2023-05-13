using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private readonly ApplicationContext _context;
        public DishRepository(ApplicationContext context) : base(context) {
            _context = context;
        }
    }
}
