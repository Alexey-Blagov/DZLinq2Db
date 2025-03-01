using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }

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
