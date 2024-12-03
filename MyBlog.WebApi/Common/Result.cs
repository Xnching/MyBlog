namespace MyBlog.WebApi.Common
{
    public class Result
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int Total { get; set; } 
        public dynamic Data { get; set; }
    }
}
