using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Contexts;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class TableRepository : GenericRepository<Tables>, ITableRepository
    {
        private readonly ApplicationContext _context;
        public TableRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
