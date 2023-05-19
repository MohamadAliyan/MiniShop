using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;

public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private readonly DbSet<Cart> _entity;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            this._entity = context.Set<Cart>();
        }
    }
