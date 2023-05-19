using EShop.Domain;

using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;

public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> _entity;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            this._entity = context.Set<Product>();
        }
    }
