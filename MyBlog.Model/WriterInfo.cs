using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MyBlog.Model
{
    public class WriterInfo:BaseId
    {
        [SugarColumn(ColumnDataType ="varchar(30)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType ="varchar(30)")]
        public string UserName { get; set; }
        [SugarColumn(ColumnDataType = "varchar(60)")]
        public string password { get; set; }
    }
}
