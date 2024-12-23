using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.WebApi.Common;
using MyBlog.Model;
using Microsoft.AspNetCore.Authorization;
using SqlSugar;
using AutoMapper;
using MyBlog.Model.DTO;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService blogNewsService;
        public BlogNewsController(IBlogNewsService blogNewsService) {
            this.blogNewsService = blogNewsService;   
        }

        [Authorize]
        [HttpGet("BlogNews")]
        //只查找自己的博客
        public async Task<ActionResult<Result>> GetBlogNews([FromServices] IMapper iMapper)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var data = await blogNewsService.QueryList(c => c.WriterId == id );
            if (data == null) return ResultHelper.Error("没查到文章！");

            try
            {
                var blogNewsDTO = iMapper.Map<List<BlogNewsDTO>>(data);
                return ResultHelper.Success(blogNewsDTO);
            }
            catch (Exception ex)
            {
                return ResultHelper.Error(ex.Message);
            }
        }

        [HttpGet("blog/{id}")]
        //查找一个博客
        public async Task<ActionResult<Result>> GetBlogNew([FromServices] IMapper iMapper, int id)
        {

            BlogNews blog = await blogNewsService.GetById(id);
            try
            {
                var blogNewsDTO = iMapper.Map<BlogNewsDTO>(blog);

                blog.BrowseCount += 1;
                await blogNewsService.Update(blog);

                // 返回 DTO
                return ResultHelper.Success(blogNewsDTO);
            }
            catch (Exception ex)
            {
                return ResultHelper.Error(ex.Message);
            }
        }

        [HttpGet("like/{id}")]
        //点赞一个博客
        public async Task<ActionResult<Result>> likeBlogNew([FromServices] IMapper iMapper, int id)
        {

            BlogNews blog = await blogNewsService.GetById(id);
            blog.LikeCount += 1;
            await blogNewsService.Update(blog);
            return ResultHelper.Success();
        }

        [HttpPost("add")]
        public async Task<ActionResult<Result>> addBlogNews(string title,string content,int typeid)
        {
            BlogNews blogNews = new BlogNews
            {
                BrowseCount = 0,
                Title = title,
                Content = content,
                TypeId = typeid,
                time = DateTime.Now,
                LikeCount = 0,
                WriterId = Convert.ToInt32(this.User.FindFirst("Id").Value),
            };
            bool b = await blogNewsService.Insert(blogNews);
            if (!b) return ResultHelper.Error("添加文章失败！");
            return ResultHelper.Success();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Result>> deleteBlogNews(int id)
        {
            bool b = await blogNewsService.DeleteById(id);
            if (!b) return ResultHelper.Error("删除失败！");
            return ResultHelper.Success();
        }

        [Authorize]
        [HttpPut("Edit")]
        public async Task<ActionResult<Result>> editBlogNews(BlogNews blog)
        {
            var blogNews = await blogNewsService.GetById(blog.Id);
            if (blogNews == null) return ResultHelper.Error("未找到文章来进行修改！");
            blogNews.Title = blog.Title;
            blogNews.Content = blog.Content;
            bool b = await blogNewsService.Update(blogNews);
            if (!b) return ResultHelper.Error("修改失败！");
            return ResultHelper.Success();
        }

        [HttpGet("page")]
        public async Task<Result> findPages([FromServices] IMapper iMapper, int pageNum,int typeId)
        {
            
            int pageSize = 10;
            RefAsync<int> total = 0;
            List<BlogNews> blogNews;
            if (typeId == 0)
            {
                blogNews = await blogNewsService.Page(pageNum, pageSize, total);
            }
            else
            {
                blogNews = await blogNewsService.QueryPage(pageNum, pageSize, total, c => c.TypeId == typeId);
            }
            try
            {
                //使用映射的，如此只得到名称，映射方法在自定义映射类中
                var blogNewsDTO = iMapper.Map<List<BlogNewsDTO>>(blogNews);
                return ResultHelper.Success(blogNewsDTO,total);
            }
            catch (Exception ex)
            {
                return ResultHelper.Error(ex.Message);
            }
        }
    }
}
