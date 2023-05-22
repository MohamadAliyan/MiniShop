using EShop.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Persistence.Configurations;

public class CartItemConfiguration : BaseConfiguration<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {

        base.Configure(builder);
    }
}