using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrGo.Data
{
    public class LinkItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public LinkItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Topic>().Wait();
        }

        public Task<List<Topic>> GetItemsAsync()
        {
            return database.Table<Topic>().ToListAsync();
        }

        public Task<List<Topic>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Topic>("SELECT * FROM [Topic]");
        }

        public Task<Topic> GetItemAsync(int id)
        {
            return database.Table<Topic>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Topic item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Topic item)
        {
            return database.DeleteAsync(item);
        }
    }
}

