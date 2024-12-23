using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Model;
using MyBlog.IService;
using MyBlog.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using MyBlog.WebApi.Filter;
namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService typeInfoService;

        public TypeController(ITypeInfoService typeInfoService)
        {
            this.typeInfoService = typeInfoService;
        }

        [TypeFilter(typeof(CustomResourceFilterAttribute))]
        [HttpGet("types")]
        public async Task<Result> getTypes()
        {
            var types = await typeInfoService.ListAll();
            types.Insert(0, new TypeInfo
            {
                Id = 0,
                Name = "全部",
            });
            return ResultHelper.Success(types);
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(DeleteFilter))] 
        public async Task<Result> addType(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return ResultHelper.Error("添加不能为空！");
            TypeInfo typeInfo = new TypeInfo
            {
                Name = name
            };
            bool b = await typeInfoService.Insert(typeInfo);
            return ResultHelper.Return(b,"添加成功","添加失败");
        }

        [HttpPut("eidt")]
        [ServiceFilter(typeof(DeleteFilter))]
        public async Task<Result> edit(int id, string name)
        {
            //如果不加await，type获得的就是Task<T>即一个任务对象，而不是T
            var type =  await typeInfoService.GetById(id);
            if (type == null) return ResultHelper.Error("在修改错误的文章！");
            type.Name = name;
            bool b = await typeInfoService.Update(type);
            return ResultHelper.Return(b, "修改成功", "修改失败");
        }

        [HttpDelete("Delete")]
        [ServiceFilter(typeof(DeleteFilter))]
        public async Task<Result> deleteType(int id)
        {
            bool b = await typeInfoService.DeleteById(id);
            return ResultHelper.Return(b, "删除成功", "删除失败");
        }
    }
}
