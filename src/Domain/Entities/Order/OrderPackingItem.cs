namespace EShop.Domain;

public class OrderPackingItem : BaseAuditableEntity
{
    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }

    public int OrderPackingId { get; set; }
    public virtual OrderPacking OrderPacking { get; set; }

}

