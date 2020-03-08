using PoeTradeDesktop.Util;
using System.Data;

namespace PoeTradeDesktop.Schemes
{
    public class StaticInfoFolder
    {
        public int Id { get; set; }
        public string Path { get; set; }

        private static string[] paths;

        public static void LoadPaths()
        {
            string query = "SELECT Path FROM StaticInfoFolder";
            DataTable dt = DB.GetDataTable(query);
            int size = dt.Rows.Count;
            paths = new string[size];
            for(int i = 0; i < size; i ++)
            {
                paths[i] = dt.Rows[i][0].ToString();
            }
        }

        public static string GetPath(short id)
        {
            if (paths == null) LoadPaths();
            return paths[id-1];
        }
    }
}