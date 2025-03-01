using LinqToDB.Data;
using LinqToDB;
using static DZLinq2Db.Program;

public class LinqToDbRepository
{
    private readonly Config _config = new Config();
    private static DataConnection? _db; 
    public LinqToDbRepository ()
    {
        _db = new LinqToDB.Data.DataConnection(ProviderName.PostgreSQL, _config.BdToken);
    }
    public static List<Customer> GetCustomer()
    {
        return _db!.GetTable<Customer>()
            .ToList();
    }
}