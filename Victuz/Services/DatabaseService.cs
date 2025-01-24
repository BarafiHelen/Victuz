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
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Event>().Wait();
            _database.CreateTableAsync<Participation>().Wait();
            _database.CreateTableAsync<QRCode>().Wait();
            _database.CreateTableAsync<Notification>().Wait();
        }

        // Algemene methoden
        
        public Task<List<T>> GetIAll<T>() where T : new()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<int> SaveItemAsync<T>(T item)
        {
            return _database.InsertOrReplaceAsync(item);
        }

        public Task<int> DeleteItemAsync<T>(T item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<List<T>> QueryItemsAsync<T>(string query, params object[] args) where T : new()
        {
            return _database.QueryAsync<T>(query, args);
        }
        
        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return _database.Table<T>().ToListAsync();
        }
    }
}