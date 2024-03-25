using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NET6.DEMO.WebApi.Utility.Filters
{
    /// <summary>
    /// 结果Filter
    /// 
    /// 如果实在ASP.NET Core 中，还有应用场景；
    /// 
    /// 
    /// 有视图的概念，如果返回的视图；渲染视图数据---生成结果； Result就可以在视图渲染之前植入逻辑，在视图渲染之后，植入逻辑；
    /// 
    /// </summary>
    public class CustomResultFilterAttribute :Attribute, IResultFilter
        //IResultFilter 接口是 ASP.NET Core 中用于结果过滤器的接口。
        //它定义了两个方法：OnResultExecuting 和 OnResultExecuted，分别在执行结果之前和之后被调用。
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("CustomResultFilterAttribute.OnResultExecuted");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("CustomResultFilterAttribute.OnResultExecuting");
        }
    }


    /// <summary>
    /// 结果Filter
    /// </summary>
    public class CustomAsyncResultFilterAttribute : Attribute, IAsyncResultFilter
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
