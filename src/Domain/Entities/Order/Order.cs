namespace EShop.Domain;

public class Order : BaseAuditableEntity
{
    public int CartId { get; set; }
    public int UserId { get; set; }
    public int DiscountPercent { get; set; }
    public int DiscountAmount { get; set; }
    public int Amount { get; set; }
    public int TotalAmount { get; set; }
    public string Address { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime Date { get; set; }

    public virtual List<OrderItem> OrderItems { get; set; }

}
