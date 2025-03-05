using LinqToDB.Data;
using LinqToDB;
using LinqToDB.Mapping;
using static DZLinq2Db.Program;
using static LinqToDB.Reflection.Methods;
namespace DZLinq2Db
{
    /// <summary>
    /// Класс подключения к БД Linq2Db. 
    /// </summary>
    public class LinqToDbRepository:IDisposable 
    {
        private DataConnection _db;
        private Config _config;
        private bool _disposed = false;
        public LinqToDbRepository()
        {
            _config = new Config();
            _db = new DataConnection(ProviderName.PostgreSQL, _config.BdToken); 
        }

        public List<Customer> GetCustomer()
        {
            return _db.GetTable<Customer>()
                .ToList();
        }
        public  int? GetCustomerId(string firstName, string lastName)
        {
            var customers = _db.GetTable<Customer>()
           .Where(o => o.FirstName == firstName && o.LastName == lastName)
           .ToList();
            if (customers.Count == 0)
            {
                Console.WriteLine("Пользователя с таким именем не найдено");
                return null;
            }
            foreach (var customerId in customers)
            {
                Console.WriteLine($"ID: {customerId.Id}, Имя: {customerId.FirstName} Фамилия:{customerId.LastName} ");
            }
            return customers[0].Id;
        }
        public void GetPriceMaxMinValue()
        {
            var products = _db.GetTable<Product>()
                .OrderBy(p => p.Price)
                .ToList();
            Console.WriteLine($"Минимальная цена товара {products[0].Name},  {products[0].Price} руб.  максимальная ценa  {products[products.Count - 1].Name}, d {products[products.Count - 1].Price} руб.");
        }
        public void GetProductPrice(decimal maxPrice)
        {
            var products = _db.GetTable<Product>()
                .Where(p => p.Price <= maxPrice)
                .ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Наименование: {product.Name} Описание: {product.Description} цена  {product.Price} руб. Доступное количество {product.StockQuantity}  ");
            }
        }
        public void GetProductList ()
        {
            var products = _db.GetTable<Product>()
                .ToList();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Наименование: {product.Name} Описание: {product.Description} цена  {product.Price} руб. Доступное количество {product.StockQuantity}  ");
            }
        }
        public void SetBuyChoiceProduct(int idcustomer, int idProducttoBuy, int productQuontity)
        {
            var Customer = _db.GetTable<Customer>().
                FirstOrDefault(p => p.Id == idcustomer);
            if (Customer is null)
            {
                Console.WriteLine($"Пользователя с таким ID не существует, зарегестрируйте пользователя");
                return; 
            }
            var Orders = _db.GetTable<Order>();
            var Product = _db.GetTable<Product>()
                .FirstOrDefault(p => p.Id == idProducttoBuy);
            if (Product != null)
            {
                if (Product.StockQuantity <= productQuontity)
                {
                    Console.WriteLine($"Недостаточно количества товара");
                }
                else
                {
                    //Оформляем покупку 
                    var order = new Order() { CustomerID = idcustomer, ProductID = idProducttoBuy, Quantity = productQuontity };
                    _db.Insert(order);
                    Product.StockQuantity -= productQuontity;
                    _db.Update(Product);  
                }
            }
            else
            {
                Console.WriteLine($"Такого товара не существует");
            }


        }
        /// <summary>
        /// Метод получения задание аналогичное ДЗ Кластерный вывод
        /// </summary>
        /// <param name="id"></param> продукта по базе 
        /// <param name="age"></param> возраст покупателя 
        public void GetCustomersByBelow30andBuyId1(int id, int age)
        {
            var Customers = _db.GetTable<Customer>();
            var Orders = _db.GetTable<Order>();
            var Products = _db.GetTable<Product>();
            var results = from c in Customers
                          join o in Orders on c.Id equals o.CustomerID
                          join p in Products on o.ProductID equals p.Id
                          where c.Age > age && p.Id == id
                          //Анонимный тип данных выборка JOIN базы
                          select new
                          {
                              CustomerID = c.Id,
                              FirstName = c.FirstName,
                              LastName = c.LastName,
                              ProductID = p.Id,
                              ProductQuantity = o.Quantity,
                              ProductPrice = p.Price
                          };
            //Блок вывода данных 
            foreach (var res in results)
            {
                Console.WriteLine($"ID: {res.CustomerID}, Фамилия: {res.LastName} Имя: {res.FirstName} \n  " +
                                  $"Id Продукта  {res.ProductID} Доступное количество {res.ProductQuantity} Цена продуката {res.ProductPrice} руб. ");
            }
        }
        /// <summary>
        /// Реализация IDisposable интерфейса 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы
                    _db?.Dispose();
                }

                _disposed = true;
            }
        }
    }

}
