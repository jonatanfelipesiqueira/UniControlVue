using System.Collections.Generic;

namespace WEB.Models.DataTables
{
    public class DataTablesOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }

        public static string OrderValue(List<DataTablesColumn> columns, List<DataTablesOrder> orders)
        {
            var orderValue = string.Empty;
            for (var i = 0; i < orders.Count; i++)
            {
                orderValue += i == orders.Count - 1
                    ? $"{columns[orders[i].Column].Name} {orders[i].Dir}"
                    : $"{columns[orders[i].Column].Name} {orders[i].Dir},";
            }

            return orderValue;
        }

        public static string OrderValue(string column, string dir)
        {
            return $"{column} {dir}";
        }
    }
}
