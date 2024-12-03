using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using MyBlog.WebApi.Common;
using MyBlog.WebApi.Utils;

namespace MyBlog.WebApi.Filter
{
    public class DeleteFilter : IActionFilter
    {
        private readonly IMemoryCache memoryCache;
        public DeleteFilter(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        //后
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // 检查Result对象是否成功
            if (context.Result is ObjectResult objectResult && objectResult.Value is Result result)
            {
                // 表示操作成功
                if (result.Code ==200 )
                {
                    string key;
                    HttpRequest request = context.HttpContext.Request;
                    string con = CacheKeyUtil.GetControllerKey(request);
                    if (con.Equals("Type"))
                    {
                        key = "Type";
                    }
                    else
                    {
                        string id = context.HttpContext.User.FindFirst("Id")?.Value;
                        key = con + "-" + id;
                    }
                    
                    memoryCache.Remove(key);
                }
            }
        }



        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
