using LinqToDB.Data;
using LinqToDB;
using static DZLinq2Db.Program;

public class LinqToDbRepository
{
    private readonly Config _config = new Config();
    private readonly DataConnection _db; 
    public LinqToDbRepository ()
    {
        _db = new LinqToDB.Data.DataConnection(ProviderName.PostgreSQL, _config.BdToken);
    }
    public static List<Customer> GetCustomer(string pattern)
    {
        var 
        return _db.GetTable<Customer>()
            .Where(x => x.FirstName == pattern)
            .OrderByDescending(x => x.FirstName)
            .ToList();
    }
}