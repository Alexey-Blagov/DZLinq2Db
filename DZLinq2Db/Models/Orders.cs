using System.ComponentModel.DataAnnotations;

public class Orders
{
    [Key]
    public int ID { get; set; }
    public int quantity { get; set; }
    public int CustomerID { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CustomerID")]
    public Customer Customer { get; set; }
    public int ProductID { get; set; }
    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ProductID")]
    public Product Product { get; set; }
}
