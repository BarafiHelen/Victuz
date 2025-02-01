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
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Participation>().Wait();
            _database.CreateTableAsync<QRCode>().Wait();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<int> SaveItemAsync<T>(T item) where T : new()
        {
            return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return await _database.DeleteAsync(item);
        }
        public async Task<QRCode> GetQRCodeAsync(int userId, int eventId)
        {
            return await _database.Table<QRCode>().FirstOrDefaultAsync(qr => qr.UserID == userId && qr.EventID == eventId);
        }
        public async Task<bool> ValidateQRCodeAsync(int userId, int eventId, string qrCodeContent)
        {
            var qrCode = await GetQRCodeAsync(userId, eventId);
            return qrCode != null && qrCode.QRCodeContent == qrCodeContent;
        }
    }
}
