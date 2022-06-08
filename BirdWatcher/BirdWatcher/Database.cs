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

        //Creates database in contructor
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Bird>().Wait();
        }

        public Task<List<Bird>> GetBirdsAsync() //Gets all birds
        {
            return _database.Table<Bird>().ToListAsync();
        }

        public Task<int> SaveBirdAsync(Bird bird) //Saves a bird
        {
            return _database.InsertAsync(bird);
        }

        public async Task<int> DeleteBird(Bird bird) //Deletes a bird
        {
            return await _database.DeleteAsync(bird);
        }

        public async Task<int> EditBird(Bird bird) //Edits a bird using new bird object
        {
            return await _database.UpdateAsync(bird);
        }

        //returns an observable collection of birds
        public async Task<ObservableCollection<Bird>> GetBirdsObservableAsync()
        {
            List<Bird> list = await _database.Table<Bird>().ToListAsync();
            ObservableCollection<Bird> result = new ObservableCollection<Bird>(list);

            return result;
        }

    }
}
