using System.Security.Cryptography;
using System.Text;

namespace MyBlog.WebApi.Utils
{
    public class MD5Util
    {
        public static string MD5Encrypt32(string password)
        {
            string pwd = "";
            MD5 mD5 = MD5.Create();
            byte[] s = mD5.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < s.Length; i++)
            {
                pwd += s[i].ToString("X");
            }
            return pwd;
        }


    }
}
