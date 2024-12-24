using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.DTO
{
    public class AddDTO
    {
        public string title { get; set; }
        public string content { get; set; }

        public int typeid { get; set; }
    }
}
