using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DcTranslate.Model
{
    public class NumberTranslationRepositoryMysql : INumberTranslationRepository
    {
        private readonly MySqlConnection _connection;

        public NumberTranslationRepositoryMysql(string host, string username, string password)
        {
            _connection = new MySqlConnection($"Server={host};Database=kamailio;Uid={username};Pwd={password};");
            _connection.Open();
        }
        public IEnumerable<NumberTranslation> GetNumberTranslations(string like = "", long page = 0)
        {
            var offset = PageSize * page;
            var whereClause = string.IsNullOrEmpty(like) ? "" : $" WHERE `from_number` LIKE '%{like}%' OR `to_number` LIKE '%{like}%' OR `description` LIKE '%{like}%'";
            var limitClause = $" LIMIT {offset},{PageSize}";
            var orderby = " ORDER BY `from_number`";
            var sql = $"SELECT `id`,`from_number`,`to_number`,`description` FROM `number_translations` {whereClause}{orderby}{limitClause}";
            var sqlCount = $"SELECT COUNT(*) FROM `number_translations` {whereClause}";
            var numberOfRowsSelectWouldHaveReturnedWithoutLimit = (long)new MySqlCommand(sqlCount, _connection).ExecuteScalar();
            LastQueryWouldHaveReturnedThisAmountOfPages = numberOfRowsSelectWouldHaveReturnedWithoutLimit / PageSize;
            var command = new MySqlCommand(sql, _connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt64(0);
                    var fromNumber = reader.GetString(1);
                    var toNumber = reader.GetString(2);
                    var description = reader.IsDBNull(3) ? null : reader.GetString(3);
                    yield return new NumberTranslation
                    {
                        Id = id,
                        FromNumber = fromNumber,
                        ToNumber = toNumber,
                        Description = description
                    };
                }
            }
        }

        public long LastQueryWouldHaveReturnedThisAmountOfPages { get; private set; }
        public long PageSize { get; set; } = 50;
        public NumberTranslation Add(NumberTranslation numberTranslation)
        {
            var sql = $"INSERT INTO `number_translations` (`from_number`,`to_number`,`description`) VALUES ('{numberTranslation.FromNumber}','{numberTranslation.ToNumber}','{numberTranslation.Description}')";
            new MySqlCommand(sql, _connection).ExecuteNonQuery();
            var id = (long)new MySqlCommand("SELECT last_insert_rowid()", _connection).ExecuteScalar();
            numberTranslation.Id = id;
            return numberTranslation;
        }

        public void Delete(NumberTranslation numberTranslation)
        {
            var sql = $"DELETE FROM `number_translations` WHERE `id = '{numberTranslation.Id}'`";
            new MySqlCommand(sql, _connection).ExecuteNonQuery();
        }

        public void Update(NumberTranslation numberTranslation)
        {
            var sql = $"UPDATE `number_translations` SET `from_number`={numberTranslation.FromNumber}, `to_number`='{numberTranslation.ToNumber}',`description`='{numberTranslation.Description}' WHERE `id`='{numberTranslation.Id}'";
            new MySqlCommand(sql, _connection).ExecuteNonQuery();
        }
    }
}