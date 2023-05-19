namespace EShop.Domain;

public class Cart : BaseAuditableEntity
{
    public int UserId { get; set; }
    public int DiscountPercent { get; set; }
    public int DiscountAmount { get; set; }
    public int Amount { get; set; }
    public int TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; }
    public virtual List<CartItem> CartItems { get; set; }


}
