namespace EShop.Domain;

public class CartItem : BaseAuditableEntity
{
    public int CartId { get; set; }
    public virtual Cart Cart { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
    public int Amount { get; set; }


}
