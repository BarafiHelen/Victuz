using SQLite;
using Victuz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Victuz.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Event>().Wait();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<int> SaveItemAsync<T>(T item) where T : new()
        {
            return await _database.InsertOrReplaceAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return await _database.DeleteAsync(item);
        }
    }
}
