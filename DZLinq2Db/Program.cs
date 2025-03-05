using LinqToDB.Data;
using LinqToDB;

namespace DZLinq2Db
{
    //Выбрать какую БД использовать(из задания "Sql запросы" или "Кластерный индекс"), написать строку подключения к БД и
    //использовать ее для подключения. (опираться можно на пример из материалов)
    //Создать классы, которые описывают таблицы в БД
    //Используя ORM выполнить простые запросы к каждой таблице, выполнить параметризованные запросы к каждой таблице
    //(без JOIN) - 2-3 запроса на таблицу.
    //Значения параметров для фильтрации можно как задавать из консоли, так и значениями переменных в коде. (пример GetStudent)
    //Выполнить все запросы, из выбранного ранее задания с передачей параметров.
    internal partial class Program
    {
        static Config _config = new Config();
        static void Main(string[] args)
        {
          
                var customer = new Customer() { Age = 19, FirstName = "Алексaндр", LastName = "Сергеев" };
                int id = 1;
                //Определиться с ORM: Dapper (Linq2Db) выбрана Linq2Db строка подключения к БД и добавление в нее данных 
                //Добавляем Customer 
                using (var conn = new DataConnection(ProviderName.PostgreSQL, _config.BdToken))
                {
                    conn.Insert(customer);  
                }
            using (var db = new LinqToDbRepository())
            {
                //Вывод данных по ФИО из репозитория полуычения Id этого пользователя 
                var customerId = db.GetCustomerId(customer.FirstName, customer.LastName);

                //Удаление данных из таблицы Customers
                try
                {
                    using (var conn = new DataConnection(ProviderName.PostgreSQL, _config.BdToken))
                    {
                        //Получаем по id юзера по id 
                        var customerbyId = conn.GetTable<Customer>().FirstOrDefault(c => c.Id == customerId);
                        if (customerbyId != null)
                        {
                            conn.Delete(customerbyId);
                            Console.WriteLine($"Пользователь ID c {customerId} удален");
                        }
                        else throw new ArgumentNullException("Не найдено такого покупателя");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //Выводим данные по максильаной и минимальной цене товара в БД
                db.GetPriceMaxMinValue();
                var price = 0m;
                var idcustomer = 0;
                var idProduct = 0;
                var productQuontity = 0;
                Console.WriteLine("Введите приемлемую цену товара");
                while (!decimal.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Не правильный ввод данных попрбуйте еще раз");
                }
                //Вывод списка товаров 
                db.GetProductPrice(price);

                Console.WriteLine();
                Console.WriteLine("Введите Id пользователя для покупки");
                while (!int.TryParse(Console.ReadLine(), out idcustomer))
                {
                    Console.WriteLine("Не правильный ввод данных попрбуйте еще раз");
                }
                Console.WriteLine();
                Console.WriteLine("Введите Id товара");
                while (!int.TryParse(Console.ReadLine(), out idProduct))
                {
                    Console.WriteLine("Не правильный ввод данных попрбуйте еще раз");
                }
                Console.WriteLine();
                Console.WriteLine("Введите количество товара");
                while (!int.TryParse(Console.ReadLine(), out productQuontity))
                {
                    Console.WriteLine("Не правильный ввод данных попрбуйте еще раз");
                }
                //Нинициируем покупку 
                db.SetBuyChoiceProduct(idcustomer, idProduct, productQuontity);
                db.GetProductList();
                Console.WriteLine();
                Task.Delay(3000);
                // Написать запрос, который возвращает список всех пользователей старше 30 лет, у которых есть заказ на продукт с ID=1.
                // В результате должны получить таблицу:CustomerID, FirstName, LastName, ProductID, ProductQuantity, ProductPrice 
                db.GetCustomersByBelow30andBuyId1(1, 30);
            }
        }
    }
}
