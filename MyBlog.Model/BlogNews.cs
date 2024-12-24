using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SqlSugar;

namespace MyBlog.Model
{
    public class BlogNews:BaseId
    {
        [SugarColumn(ColumnDataType ="varchar(30)")]
        public string Title { get; set; }
        [SugarColumn(ColumnDataType ="text")]
        public string Content { get; set; }
        public DateTime time { get; set; }
        public int BrowseCount {  get; set; }
        public int LikeCount {  get; set; }
        public int TypeId {  get; set; }
        public int WriterId {  get; set; }

        /// <summary>
        /// 附带的、对应映射的表里不存在的属性
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [JsonIgnore]
        public TypeInfo TypeInfo { get; set; }
        [SugarColumn(IsIgnore = true)]
        [JsonIgnore]
        public WriterInfo WriterInfo { get; set; }
    }
}
