using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SniffExplorer.Parsing.Extensions
{
    public static class MiscExtensions
    {
        public static int IndexOf<T>(this T[] self, T value)
        {
            for (var i = 0; i < self.Length; ++i)
                if (EqualityComparer<T>.Default.Equals(self[i], value))
                    return i;

            return -1;
        }

        public static SqliteConnection AsDatabase(this byte[] resource)
        {
            var tempFileName = Path.GetTempFileName();
            using (var tempFile = new FileStream(tempFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
            {
                tempFile.Write(resource);
                tempFile.Flush();

                var builder = new SqliteConnectionStringBuilder {
                    Mode = SqliteOpenMode.ReadOnly,
                    DataSource = Path.GetFullPath(tempFileName)
                };


                var connection = new SqliteConnection(builder.ToString());
                connection.StateChange += (sender, args) => {
                    if (args.CurrentState == ConnectionState.Closed)
                        File.Delete(tempFileName);
                };
                return connection;
            }
        }
    }
}
