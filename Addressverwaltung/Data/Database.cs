using SQLite;
using Adressverwaltung.Models;

namespace Adressverwaltung.Data;

public class Database
{
    readonly SQLiteAsyncConnection _database;

    public Database(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Address>().Wait();
    }

    public Task<List<Address>> GetAddressesAsync()
        => _database.Table<Address>().ToListAsync();

    public Task<Address> GetAddressAsync(int id)
        => _database.Table<Address>().Where(a => a.Id == id).FirstOrDefaultAsync();

    public Task<int> SaveAddressAsync(Address address)
    {
        if (address.Id != 0)
            return _database.UpdateAsync(address);
        else
            return _database.InsertAsync(address);
    }

    public Task<int> UpdateAddressAsync(Address address)
        => _database.UpdateAsync(address);

    public Task<int> DeleteAddressAsync(Address address)
        => _database.DeleteAsync(address);
}
