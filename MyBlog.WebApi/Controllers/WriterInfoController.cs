using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.WebApi.Common;
using MyBlog.WebApi.Filter;
using MyBlog.WebApi.Utils;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService writerInfoService;
        public WriterInfoController(IWriterInfoService writerInfoService)
        {
            this.writerInfoService = writerInfoService;
        }

        [HttpPost("add")]
        public async Task<Result> addWriter(string name,string username,string userpwd)
        {
            var oldWriter = await writerInfoService.QueryOne(c => c.Name == username);
            if (oldWriter != null) return ResultHelper.Error("账号已存在！");
            WriterInfo writerInfo = new WriterInfo
            {
                Name = name,
                UserName = username,
                password = MD5Util.MD5Encrypt32(userpwd)
            };

            bool b = await writerInfoService.Insert(writerInfo);
            return ResultHelper.Return(b, "添加成功", "添加失败");
        }

        [HttpPut("edit")]
        [ServiceFilter(typeof(DeleteFilter))]
        public async Task<Result> editName(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writerInfo = await writerInfoService.GetById(id);
            writerInfo.Name = name;
            bool b = await writerInfoService.Update(writerInfo);
            return ResultHelper.Return(b, "修改名称成功", "修改名称失败");
        }

        [AllowAnonymous]
        [TypeFilter(typeof(CustomResourceFilterAttribute))]
        [HttpGet("findWriter")]
        public async Task<Result> findWriter([FromServices]IMapper iMapper,int id)  //fromServices意味着不从类构造函数中注入，而是从方法中注入，即只能该方法使用
        {
            var writer = await writerInfoService.GetById(id);
            var writerDTO = iMapper.Map<WriterDTO>(writer);
            return ResultHelper.Success(writerDTO);
        }
    }
}
