using Microsoft.AspNetCore.Mvc.Filters;
using NLog.Filters;

namespace NET6.DEMO.WebApi.Utility.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomAlwayRunResultFilterAttribute :Attribute, IAlwaysRunResultFilter
        //IAlwaysRunResultFilter 是一个接口，用于表示一个结果过滤器（Result Filter），它将始终运行，无论结果是否被缓存。
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("IAlwayRunResultFilterAttribute.OnResultExecuted");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("IAlwayRunResultFilterAttribute.OnResultExecuting");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomAsyncAlwayRunResultFilterAttribute : IAsyncAlwaysRunResultFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Console.WriteLine("CustomAsyncResultFilterAttribute.OnResultExecutionAsync");
            {
                //生成结果前植入逻辑
            }
            await next.Invoke();//生成结果；
            {
                //生成结果后植入逻辑
            }
        }
    }

}
