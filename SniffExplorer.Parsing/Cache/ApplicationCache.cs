using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace SniffExplorer.Parsing.Cache
{
    public class ApplicationCache
    {
        public static Lazy<ApplicationCache> Instance { get; } = new(() => new ApplicationCache("Cache.db"));

        private readonly SqliteConnection _connection;

        private ApplicationCache(string cacheFile)
        {
            var builder = new SqliteConnectionStringBuilder() {
                DataSource = cacheFile,
                Mode = SqliteOpenMode.ReadWrite
            };

            _connection = new SqliteConnection(builder.ToString());
            _connection.Open();
            if (_connection.State != ConnectionState.Open)
            {
                using var fileStream = File.Create(cacheFile);
                _connection.Open();
            }
        }

        ~ApplicationCache()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
