using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public List<Product> Products { get; set; } = new List<Product>(); 
}
//--Products(ID, Name, Description, StockQuantity, Price)
//CREATE TABLE Products (
//    ID SERIAL PRIMARY KEY,
//    Name VARCHAR(20) NOT NULL,
//    Description VARCHAR(20) NOT NULL,
//    StockQuantity int ,
//    Price DECIMAL(10, 2)
//);
//--Orders(ID, CustomerID, ProductID, Quantity)
//CREATE TABLE Orders (
//    ID SERIAL PRIMARY KEY,
//    CustomerID INT,
//    ProductID INT,
//    Quantity INT DEFAULT 0 CHECK (Quantity >= 0),
//    CONSTRAINT fk_customer
//        FOREIGN KEY (CustomerID) 
//        REFERENCES Customers(ID),
//    CONSTRAINT fk_product
//        FOREIGN KEY (ProductID) 
//        REFERENCES Products(ID)
//);
