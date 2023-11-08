using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddresBook.Classes
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _conn;

        public Database(string dbPath)
        {
            _conn = new SQLiteAsyncConnection(dbPath);
            _conn.CreateTableAsync<AddresData>().Wait();
        }

        public Task<int> AddAddressData(AddresData addres)
        {
            return _conn.InsertAsync(addres);
        }

        public Task<int> UpdateAddressData(AddresData addres)
        {
            return _conn.UpdateAsync(addres);
        }

        public Task<int> DeleteAddressData(AddresData addres)
        {
            return _conn.DeleteAsync(addres);
        }

        public Task<List<AddresData>> GetAll() 
        {
            return _conn.Table<AddresData>().ToListAsync();
        }


    }
}
