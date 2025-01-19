using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victuz.Models;

namespace Victuz
{
    public class DatabaseService
    {
        private readonly SQLiteConnection _database; // Databaseverbinding

        // Constructor: databasepad wordt expliciet meegegeven
        public DatabaseService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);

            // Maak de tabellen aan
            _database.CreateTable<Models.User>();
            _database.CreateTable<Models.Admin>();
            _database.CreateTable<Models.Event>();
            _database.CreateTable<Models.Participation>();
            _database.CreateTable<Models.Notification>();

            Console.WriteLine("Database succesvol geïnitialiseerd.");
        }

        // CRUD-methoden
        public List<T> GetAll<T>() where T : new()
        {
            return _database.Table<T>().ToList();
        }

        public void Save<T>(T item)
        {
            _database.Insert(item);
            Console.WriteLine($"{typeof(T).Name} opgeslagen in de database.");
        }

        public void Update<T>(T item)
        {
            _database.Update(item);
            Console.WriteLine($"{typeof(T).Name} bijgewerkt in de database.");
        }

        public void Delete<T>(T item)
        {
            _database.Delete(item);
            Console.WriteLine($"{typeof(T).Name} verwijderd uit de database.");
        }
    }
}
