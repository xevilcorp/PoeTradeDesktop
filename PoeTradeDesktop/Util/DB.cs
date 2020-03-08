using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace PoeTradeDesktop.Util
{
    public class DB
    {
        //private static SQLiteConnection sqliteConnection;
        public static FileInfo path = new FileInfo(Path.Combine(Environment.CurrentDirectory + "/Data/Db.sqlite"));

        private static SQLiteConnection DbConnection()
        {
            return new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = $@"{path}", ForeignKeys = true }.ConnectionString
            };
        }

        public static DataTable GetDataTable(string query)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = query;
                da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                da.Fill(dt);
                return dt;
            }
        }

        public static void ExecuteCMD(string query)
        {

            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }

        }

    }
}
