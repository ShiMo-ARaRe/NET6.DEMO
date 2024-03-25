using Microsoft.AspNetCore.Mvc.Filters;
using static System.Net.Mime.MediaTypeNames;

namespace NET6.DEMO.WebApi.Utility.Filters
{
    /// <summary>
    /// 自定义AsyncResouceFilter
    /// </summary>
    public class CustomAsyncResourceFilterAttribute : Attribute, IAsyncResourceFilter
        //IAsyncResourceFilter 接口是 ASP.NET Core 中的一个接口，用于定义异步资源过滤器。
   
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            //返回类型：Task，表示异步操作。
        //参数 next：类型为 ResourceExecutionDelegate，表示资源执行的委托。
        //在该方法内部，调用 next.Invoke() 可以继续执行后续的资源过滤器和最终的资源执行逻辑。
        {
            Console.WriteLine("======Before==CustomAsyncResourceFilterAttribute.OnResourceExecutionAsync===========");
            Console.WriteLine("======Before==CustomAsyncResourceFilterAttribute.OnResourceExecutionAsync===========");

            await next.Invoke();    //执行顺序：上面代码-->控制器构造函数-->API方法-->下面代码

            //在资源执行之前，输出两条日志信息，提示开始执行过滤器逻辑。
            //调用 next.Invoke()，继续执行后续的资源过滤器和最终的资源执行逻辑。
            //在资源执行之后，输出两条日志信息，提示完成过滤器逻辑。


            Console.WriteLine("======After==CustomAsyncResourceFilterAttribute.OnResourceExecutionAsync===========");
            Console.WriteLine("======After==CustomAsyncResourceFilterAttribute.OnResourceExecutionAsync===========");
        }
    }
}
