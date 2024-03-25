using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET6.DEMO.Interfaces;

namespace NET6.DEMO.WebApi.Utility.Filters
{

    /// <summary>
    /// CustomAsyncExceptionFilterAttribute
    /// </summary>
    public class CustomAsyncExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
        //IAsyncExceptionFilter 是 ASP.NET Core 中的一个接口，用于实现异步的自定义异常过滤器。
    {
        /// <summary>
        /// 发生异常后就在这里处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //在这里就应该处理异常
            if (context.ExceptionHandled == false) //如果没有被处理
            {
                context.Result = new JsonResult(new ApiResult<object>()
                {
                    Success = false,
                    Message = context.Exception.Message
                });
                //就在这里处理
                context.ExceptionHandled = true;//异常已经被处理过了；
            }

            /* Task.CompletedTask 是一个已完成的 Task 对象。它表示一个不执行任何操作并已经完成的任务。
            使用 Task.CompletedTask 可以避免创建额外的任务对象，特别是在不需要执行任何异步操作时。
            它可以在异步方法中作为返值，表示该方法已经完成，不需要进行进一步的异步操作。*/
            await Task.CompletedTask;
                /*  与 IExceptionFilter 中的 OnException 方法相比，IAsyncExceptionFilter 
                中的 OnExceptionAsync 方法允许在方法内部执行异步操作。
                这对于需要进行异步处理的异常情况非常有用，例如通过异步方式记录日志、调用异步方法等。
                使用 IAsyncExceptionFilter 可以确保在异常处理过程中不会阻塞主线程，提高应用程序的性能和响应能力。*/
        }
    }
}
