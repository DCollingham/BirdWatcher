using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public async Task<int> DeleteBird(Bird bird)
        {
            return await _database.DeleteAsync(bird);
        }

        public async Task<int> EditBird(Bird bird)
        {
            return await _database.UpdateAsync(bird);
        }

        public ObservableCollection<Bird> GetBirdsObservableAsync()
        {
            List<Bird> list = _database.Table<Bird>().ToListAsync().Result;
            ObservableCollection<Bird> result = new ObservableCollection<Bird>(list);

            return result;
        }

    }
}
