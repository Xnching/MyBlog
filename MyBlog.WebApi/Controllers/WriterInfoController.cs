using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Service;
using MyBlog.WebApi.Common;
using MyBlog.WebApi.Filter;
using MyBlog.WebApi.Utils;
using SqlSugar;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService writerInfoService;
        public WriterInfoController(IWriterInfoService writerInfoService)
        {
            this.writerInfoService = writerInfoService;
        }

        [HttpPost("add")]
        public async Task<Result> addWriter(RegisterDTO register)
        {
            var oldWriter = await writerInfoService.QueryOne(c => c.UserName == register.username);
            if (oldWriter != null) return ResultHelper.Error("账号已存在！");
            WriterInfo writerInfo = new WriterInfo
            {
                Name = register.name,
                UserName = register.username,
                password = MD5Util.MD5Encrypt32(register.userpwd)
            };

            bool b = await writerInfoService.Insert(writerInfo);
            return ResultHelper.Return(b, "添加成功", "添加失败");
        }

        [Authorize]
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

        [Authorize]
        [HttpGet("page")]
        public async Task<Result> findPages([FromServices] IMapper iMapper, int pageNum)
        {
            int pageSize = 5;
            RefAsync<int> total = 0;
            List<WriterInfo> infos;
            
            infos = await writerInfoService.Page(pageNum, pageSize, total);
            
            try
            {
                //使用映射的，如此只得到名称，映射方法在自定义映射类中
                var blogNewsDTO = iMapper.Map<List<WriterDTO>>(infos);
                return ResultHelper.Success(blogNewsDTO, total);
            }
            catch (Exception ex)
            {
                return ResultHelper.Error(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Result>> deleteWriterInfo(int id)
        {
            bool b = await writerInfoService.DeleteById(id);
            if (!b) return ResultHelper.Error("删除失败！");
            return ResultHelper.Success();
        }
    }
}
