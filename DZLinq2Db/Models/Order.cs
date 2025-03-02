using LinqToDB.Mapping;
[Table(Name = "orders")]
public class Order
{
    [PrimaryKey, Identity]
    [Column(Name = "id")]
    public int Id { get; set; }
    [Column(Name = "quantity")]
    public int Quantity { get; set; }
    [Column(Name = "customerid")]
    public int CustomerID { get; set; }
    [Column(Name = "productid")]
    public int ProductID { get; set; }
}
