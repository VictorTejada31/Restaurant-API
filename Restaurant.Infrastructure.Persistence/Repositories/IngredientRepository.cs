using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        private readonly ApplicationContext _context;
        public IngredientRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
