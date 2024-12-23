using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.DTO
{
    public class LoginDTO
    {
        public string Jwt { get; set; }
        public WriterDTO UserInfo { get; set; }
    }
}
