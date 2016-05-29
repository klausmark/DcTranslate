using System.Collections.Generic;
using System.Data.SQLite;

namespace DcTranslate.Model
{
    public class NumberTranslationRepositorySqliteDemo : INumberTranslationRepository
    {
        private readonly SQLiteConnection _connection;
        public NumberTranslationRepositorySqliteDemo()
        {
            _connection = new SQLiteConnection(@"Data Source=..\..\kamailio.db;Version=3;");
            _connection.Open();
            var sql = "CREATE TABLE IF NOT EXISTS `number_translations` (`id` INTEGER PRIMARY KEY, `from_number` TEXT UNIQUE NOT NULL, `to_number` TEXT NOT NULL, `description` TEXT)";
            var command = new SQLiteCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public IEnumerable<NumberTranslation> GetNumberTranslations(string like = "", long page = 0)
        {
            var offset = PageSize * page;
            var whereClause = string.IsNullOrEmpty(like) ? "" : $" WHERE `from_number` LIKE '%{like}%' OR `to_number` LIKE '%{like}%' OR `description` LIKE '%{like}%'";
            var limitClause = $" LIMIT {offset},{PageSize}";
            var orderby = " ORDER BY `from_number`";
            var sql = $"SELECT `id`,`from_number`,`to_number`,`description` FROM `number_translations` {whereClause}{orderby}{limitClause}";
            var sqlCount = $"SELECT COUNT(*) FROM `number_translations` {whereClause}";
            var numberOfRowsSelectWouldHaveReturnedWithoutLimit = (long) new SQLiteCommand(sqlCount, _connection).ExecuteScalar();
            LastQueryWouldHaveReturnedThisAmountOfPages = numberOfRowsSelectWouldHaveReturnedWithoutLimit/PageSize;
            if (numberOfRowsSelectWouldHaveReturnedWithoutLimit%PageSize != 0)
                LastQueryWouldHaveReturnedThisAmountOfPages++;
            var command = new SQLiteCommand(sql, _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                var fromNumber = reader.GetString(1);
                var toNumber = reader.GetString(2);
                var description = reader.IsDBNull(3) ? null : reader.GetString(3);
                yield return new NumberTranslation
                {
                    Id=id,
                    FromNumber = fromNumber,
                    ToNumber = toNumber,
                    Description = description
                };
            }
        }

        public long LastQueryWouldHaveReturnedThisAmountOfPages { get; set; }
        public long PageSize { get; set; } = 50;

        public NumberTranslation Add(NumberTranslation numberTranslation)
        {
            var sql = $"INSERT INTO `number_translations` (`from_number`,`to_number`,`description`) VALUES ('{numberTranslation.FromNumber}','{numberTranslation.ToNumber}','{numberTranslation.Description}')";
            new SQLiteCommand(sql, _connection).ExecuteNonQuery();
            var id = (long)new SQLiteCommand("SELECT last_insert_rowid()", _connection).ExecuteScalar();
            numberTranslation.Id = id;
            return numberTranslation;
        }

        public void Remove(NumberTranslation numberTranslation)
        {
            var sql = $"DELETE FROM `number_translations` WHERE `id = '{numberTranslation.Id}'`";
            new SQLiteCommand(sql, _connection).ExecuteNonQuery();
        }

        public void Update(NumberTranslation numberTranslation)
        {
            var sql = $"UPDATE `number_translations` SET `from_number`={numberTranslation.FromNumber}, `to_number`='{numberTranslation.ToNumber}',`description`='{numberTranslation.Description}' WHERE `id`='{numberTranslation.Id}'";
            new SQLiteCommand(sql, _connection).ExecuteNonQuery();
        }
    }
}