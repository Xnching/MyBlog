using SqlSugar;
using static Dm.net.buffer.ByteArrayBuffer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyBlog.WebApi.Common
{
    public static class ResultHelper
    {
        public static Result Success(dynamic data)
        {
            return new Result
            {
                Code = 200,
                Data = data,
                Message = "成功！",
                Total = 0
            };
        }
        public static Result Success()
        {
            return new Result
            {
                Code = 200,
                Data = null,
                Message = "",
                Total = 0
            };
        }
        public static Result Success(dynamic data,RefAsync<int> total)
        {
            return new Result
            {
                Code = 200,
                Data = data,
                Message = "成功！",
                Total = total
            };
        }

        public static Result Error(string msg)
        {
            return new Result
            {
                Code = 500,
                Data = null,
                Message = msg,
                Total = 0
            };
        }

        public static Result Return(bool b,string successMsg,string errorMsg)
        {
            if (b)
            {
                return new Result
                {
                    Code = 200,
                    Data = null,
                    Message = successMsg,
                    Total = 0
                };
            }
            return new Result
            {
                Code = 500,
                Data = null,
                Message = errorMsg,
                Total = 0
            };
        }

    }
}
