using EShop.Domain;
using EShop.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Persistence.Configurations;


public class BaseConfiguration<T> :  IEntityTypeConfiguration<T> where T : BaseAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {

        builder.HasQueryFilter(p => !p.IsDeleted);

    }
}

public class OrderConfiguration : BaseConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        
        base.Configure(builder);

    }

}

public class OrderItemConfiguration : BaseConfiguration<OrderItem>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        

    }
}
public class CartConfiguration : BaseConfiguration<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        

    }
}
public class CartItemConfiguration : BaseConfiguration<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        

    }
}