using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;

public class OrderPackingItemRepository : BaseRepository<OrderPackingItem>, IOrderPackingItemRepository
    {
        private readonly DbSet<OrderPackingItem> _entity;
        public OrderPackingItemRepository(ApplicationDbContext context) : base(context)
        {
            this._entity = context.Set<OrderPackingItem>();
        }
    }
