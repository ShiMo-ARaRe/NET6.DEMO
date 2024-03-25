using Microsoft.AspNetCore.Mvc.Filters;

namespace NET6.DEMO.WebApi.Utility.Filters
{

    /// <summary>
    /// ActionFilter异步版本扩展定制
    /// </summary>
    public class CustomAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            {   
                //如果在这里植入业务逻辑，就是子API执行之前扩展业务逻辑
            }
            await next.Invoke();  //这里了就是他要执行API // 执行顺序：控制器构造函数-->上面代码-->API方法-->下面代码
            {
                //如果在这里植入业务逻辑，就是子API执行之后扩展业务逻辑
            }
        }
    }
}
