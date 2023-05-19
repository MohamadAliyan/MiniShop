namespace EShop.Domain;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public int Price { get; set; }
    public int Benefit { get; set; }

}
