namespace EShop.Domain;

public class OrderPacking : BaseAuditableEntity
{
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
    public int Cost { get; set; }
    public int TotalAmount { get; set; }
    public ShippingMethodType ShippingMethodType { get; set; }
    public List<OrderPackingItem> OrderPackingItems { get; set; }


}

