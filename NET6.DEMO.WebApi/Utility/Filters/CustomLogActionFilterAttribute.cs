using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace NET6.DEMO.WebApi.Utility.Filters
{

    /// <summary>
    /// ActionFilter扩展定制
    /// </summary>
    public class CustomLogActionFilterAttribute : Attribute, IActionFilter
        //IActionFilter 是 ASP.NET Core 中的一个过滤器接口，用于在控制器 动作方法 执行前后执行自定义逻辑。


        /* ActionFilter 适合什么场景应用呢？ 缓存？---也可以扩展缓存；
        为什么说 ResourceFilter 更适合做缓存？--- ResouceFilter 做缓存性能更高； 
        请求处理的环节会更少，所以 ResouceFilter 更适合做缓存；
        ActionFilter 究竟适合做什么呢？ ActionFilter --靠近 API 方法； 传入到 API 的参数， API 执行
        结束后，执行结果；都是 ActionFilter 最先获取到；记录日志的时候；需要记录下来；*/

    {

        private readonly ILogger<CustomLogActionFilterAttribute> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CustomLogActionFilterAttribute(ILogger<CustomLogActionFilterAttribute> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 在XX执行Action执行之后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
           /* context.RouteData 是一个对象，它包含有关当前请求的路由数据，包括控制器名、动作名和其他路由参数等信息。
            Values 是 RouteData 对象的属性，它是一个键值对集合，用于存储路由数据的键值对。
            ["controller"] 和["action"] 是用于访问控制器名和动作名的键。
            通过使用这些键，您可以从 RouteData.Values 集合中获取相应的值。*/
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            //使用日志记录器（_logger）记录一条日志消息的代码。该日志消息包含了控制器名和动作名
            _logger.LogInformation($"在{controllerName}--{actionName} 执行之后====");
            //这里是执行完API之后，在这里执行业务逻辑---这里可以记录计算结果
            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuted");
        }

        /// <summary>
        ///  在XXAction执行之前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
          
            _logger.LogInformation($"在{controllerName}--{actionName} 执行之前====");
            //这里是执行完API之前，在这里执行业务逻辑--这可以记录参数
            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuting");
        }
    }
}
