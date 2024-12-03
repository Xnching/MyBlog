namespace MyBlog.WebApi.Utils
{
    public class CacheKeyUtil
    {
        public static string GetRouteKey(HttpRequest request)
        {
            string route = request.QueryString.Value; // 参数，例如 ?name=name

            // 提取查询字符串参数的值
            var queryParameters = request.Query;
            if (!queryParameters.Any())
            {
                return string.Empty; // 或者返回一个默认值
            }

            var parameterValues = queryParameters
                .Select(kvp => kvp.Value.ToString()) // 获取参数值
                .Aggregate((accumulated, next) => accumulated + "," + next); // 将所有参数值合并成一个字符串

            // 构建key，只包含api后面的路径和参数的值
            return parameterValues;
        }


        public static string GetControllerKey(HttpRequest request)
        {
            string path = request.Path.Value; // 路径，例如 /api/Type/edit

            // 获取api后面的第一个//里的内容
            int apiIndex = path.IndexOf("/api/", StringComparison.OrdinalIgnoreCase);
            if (apiIndex != -1)
            {
                path = path.Substring(apiIndex + "/api/".Length);
            }

            // 获取第一个"/"之前的部分，即控制器名称
            string controllerName = path.Split('/').First(); // 获取路径的第一段，例如 "Type"

            return controllerName;
        }

        public static string BuildCacheKey(HttpRequest request)
        {
            string con = GetControllerKey(request);
            if (con.Equals("Type")) return "Type";
            return con+"-"+GetRouteKey(request);
        }
    }
}
