using ET_Backend.Extensions;
using Microsoft.Data.Sqlite;
using Xunit;

namespace ET_UnitTests.Unittests
{
    public class SqliteExtensionsTests
    {
        [Fact]
        public void WithForeignKeys_EnablesForeignKeys()
        {
            using var conn = new SqliteConnection("Data Source=:memory:");
            var resultConn = conn.WithForeignKeys();

            using var cmd = resultConn.CreateCommand();
            cmd.CommandText = "PRAGMA foreign_keys;";
            var foreignKeysOn = cmd.ExecuteScalar();

            Assert.Equal(1L, foreignKeysOn); // SQLite gibt 1 (long) zurück, wenn Foreign Keys aktiv sind
        }
    }
}
