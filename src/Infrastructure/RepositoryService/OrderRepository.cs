using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly DbSet<Order> _entity;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            this._entity = context.Set<Order>();
        }
    }
