using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.WebApi.Common;
using MyBlog.WebApi.Utils;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace MyBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IWriterInfoService writerInfoService;
        public AuthoizeController(IWriterInfoService writerInfoService)
        {
            this.writerInfoService = writerInfoService;
        }
        [HttpPost("login")]
        public async Task<Result> login(string username, string password)
        {
            string pwd = MD5Util.MD5Encrypt32(password);
            var writer = await writerInfoService.QueryOne(c => (c.UserName == username) && (c.password == pwd));
            if (writer != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,writer.Name),
                    new Claim("Id",writer.Id.ToString()),
                    new Claim("UserName",writer.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: "SDMC-CJAS1-SAD-DFSFA-SADHJVF-VFF"));
                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:6060",
                    audience: "http://localhost:5034",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(value: 48),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ResultHelper.Success(jwtToken);
            }
            else
            {
                return ResultHelper.Error("账号或密码错误！");
            }
        
        }
    }
}
