using Microsoft.Extensions.Configuration;

namespace DZLinq2Db
{
    internal partial class Program
    {
        public class Config 
        {
            private readonly string _filePath;
            private string _bdToken;
            public string BdToken
            {
                get
                {
                    return _bdToken!;
                }
            }
            public Config()
            {
                _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PassToken.json");
                try
                {
                    LoadFromJson();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            private void LoadFromJson()
            {
                if (File.Exists(_filePath))
                {
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("PassToken.json", optional: false, reloadOnChange: true)
                        .Build();
                        _bdToken = configuration["BdToken"] ?? throw new InvalidDataException("Поле BdToken отсутствует в файле.");
                }
            }
        }
    }
}
//"Host=localhost;" +
//                "Port=5432;" +
//                "Database=DriverVehicleRouteDb;" +
//                "Username=postgres;" +
//                "Password=20071978");