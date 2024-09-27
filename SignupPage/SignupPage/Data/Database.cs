using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignupPage.Models;

namespace SignupPage.Data
{
    internal class Database
    {
        private readonly SQLiteAsyncConnection _connection;

        public Database() 
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "MauiTodo.db");

            string _dbEncryptionKey = SecureStorage.GetAsync("dbkey").Result;

            if (string.IsNullOrEmpty(_dbEncryptionKey))
            {
                Guid g = new Guid();
                _dbEncryptionKey = g.ToString();
                SecureStorage.SetAsync("dbkey", _dbEncryptionKey);
            }

            var dbOptions = new SQLiteConnectionString(databasePath, true, key: _dbEncryptionKey);

            _connection = new SQLiteAsyncConnection(dbOptions);

            _ = Initialise();
        }

        private async Task Initialise()
        {
            await _connection.CreateTableAsync<User>();
        }

        public async Task<User> GetUser(String email)
        {
            var query = _connection.Table<User>().Where(t => t.Email == email);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> AddUser(User user)
        {
            return await _connection.InsertAsync(user);
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            // Simulating validation of user credentials
            User user = await GetUser(email);
            if (user != null) 
            {
                return user.Password == password;
            }
            return false;
        }
    }
}
