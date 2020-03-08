using PoeTradeDesktop.Util;
using System;
using System.Data;

namespace PoeTradeDesktop.Schemes
{
    public class StaticInfo
    {
        public string Text { get; set; }
        public string Image { get; set; }

        public static StaticInfo Get(string id)
        {
            string query = $"SELECT Text, Image, FolderId FROM StaticInfo WHERE Id='{id}'";
            DataTable dt = DB.GetDataTable(query);
            return new StaticInfo
            {
                Text = dt.Rows[0][0].ToString(),
                Image = StaticInfoFolder.GetPath(Convert.ToInt16(dt.Rows[0][2])) + dt.Rows[0][1].ToString()
            };  
            
        }

    }
}
