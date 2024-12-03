using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("NoAutorize")]
        public string NoAutorize()
        {
            return "No Autorize";
        }

        [Authorize]
        [HttpGet("Autorize")]
        public string Autorize()
        {
            return "has Autorize";
        }
    }
}
