using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using MyBlog.WebApi.Utils;

namespace MyBlog.WebApi.Filter
{
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter     //有两个
    {
        private readonly IMemoryCache memoryCache;
        public CustomResourceFilterAttribute(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        //处理完后
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string key = CacheKeyUtil.BuildCacheKey(context.HttpContext.Request);
            memoryCache.Set(key, context.Result);
        }


        //处理完前
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string key = CacheKeyUtil.BuildCacheKey(context.HttpContext.Request);

            if (memoryCache.TryGetValue(key, out object value))
            {
                context.Result = value as IActionResult;
            }
        }
    }
}
