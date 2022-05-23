using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirdWatcher
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Bird>().Wait();
        }

        public Task<List<Bird>> GetBirdsAsync()
        {
            return _database.Table<Bird>().ToListAsync();
        }

        public Task<int> SaveBirdAsync(Bird bird)
        {
            return _database.InsertAsync(bird);
        }


    }
}
