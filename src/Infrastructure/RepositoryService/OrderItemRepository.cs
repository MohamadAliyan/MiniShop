using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;

public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        private readonly DbSet<OrderItem> _entity;
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            this._entity = context.Set<OrderItem>();
        }
    }
