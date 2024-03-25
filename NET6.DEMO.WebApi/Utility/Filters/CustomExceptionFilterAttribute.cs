using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET6.DEMO.Interfaces;

namespace NET6.DEMO.WebApi.Utility.Filters
{

    /// <summary>
    /// CustomExceptionFilterAttribute
    /// </summary>
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
        //IExceptionFilter 是 ASP.NET Core 中的一个接口，用于实现自定义的异常过滤器。
        //异常过滤器可以在发生异常时进行捕获和处理。
    {

        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 当有异常发生的时候，触发这个方法
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context) // 该方法在发生异常时被调用并提供异常上下文信息。
        { 
            _logger.LogInformation(context.Exception.Message); // 记录异常信息，将异常消息写入日志中。

            //在这里就应该处理异常
            if (context.ExceptionHandled == false) //如果没有被处理
                // 这里创建了一个 JsonResult 对象，将 ApiResult<object> 对象作为结果返回给客户端。
            {
                context.Result = new JsonResult(new ApiResult<object>()
                {
                    Success = false,
                    Message = context.Exception.Message // 错误信息
                });
                //就在这里处理
                context.ExceptionHandled = true;//异常已经被处理过了；
            }
        }
    }
}
