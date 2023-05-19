using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;

public class OrderPackingRepository : BaseRepository<OrderPacking>, IOrderPackingRepository
    {
        private readonly DbSet<OrderPacking> _entity;
        public OrderPackingRepository(ApplicationDbContext context) : base(context)
        {
            this._entity = context.Set<OrderPacking>();
        }
    }
