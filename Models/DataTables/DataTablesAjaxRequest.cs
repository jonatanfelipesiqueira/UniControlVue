using System.Collections.Generic;

namespace WEB.Models.DataTables
{
    public class DataTablesAjaxRequest
    {
        public int Draw { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }
        public string Busca { get; set; }
        public DataTablesSearch Search { get; set; }
        public List<DataTablesColumn> Columns { get; set; }
        public List<DataTablesOrder> Order { get; set; }
    }
}
