using Microsoft.AspNetCore.Mvc.Filters;

namespace NET6.DEMO.WebApi.Utility.Filters
{

    /// <summary>
    /// ActionFilter扩展定制
    /// </summary>
    public class CustomActionFilterAttribute : Attribute, IActionFilter
        //IActionFilter 是 ASP.NET Core 中的一个过滤器接口，用于在控制器 动作方法 执行前后执行自定义逻辑。
    {
        /// <summary>
        /// 在XX执行Action执行之后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuted");
        }

        //执行顺序：控制器构造函数-->OnActionExecuting方法-->API方法-->OnActionExecuted方法

        /// <summary>
        /// 在XXAction执行之前
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuting");
        }
    }
}
