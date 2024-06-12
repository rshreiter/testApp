using System.Collections.Generic;
using System.Linq;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace SourceGen
{
    public class PostgreDataProvider
    {
        public List<string> GetSimpleData()
        {
            using (var con =
                   new NpgsqlConnection("Host=localhost:5432;Username=postgres;Password=1234;Database=db"))
            {
                var pc = new PostgresCompiler();
                var db = new QueryFactory(con, pc);
                var result = db.Query("classes").Select("name").Get<string>().ToList();
                result = result.Select(x => x.Trim()).ToList();

                return result;
            }
        }
    }
}