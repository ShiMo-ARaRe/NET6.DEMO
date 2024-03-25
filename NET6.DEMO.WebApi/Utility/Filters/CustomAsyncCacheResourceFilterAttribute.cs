using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NET6.DEMO.WebApi.Utility.Filters
{
    /// <summary>
    /// 自定义ResouceFilter扩展缓存
    /// </summary>
    public class CustomAsyncCacheResourceFilterAttribute : Attribute, IAsyncResourceFilter
    {
        /// <summary>
        /// 缓存区域
        /// </summary>
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        { 
            //判断缓存
            string key = context.HttpContext.Request.Path;//Url地址
            if (CacheDictionary.ContainsKey(key))
            {
                object oResult = CacheDictionary[key];
                IActionResult result = oResult as IActionResult;
                context.Result = result;  //请求处理的过程中的一个短路器，如果给Result赋值了，就不继续往后执行了，如果没有赋值，为null,就继续往后执行；
            }
            else
            {
                //next.Invoke() 方法的返回类型是 Task<ResourceExecutedContext>，
                //因此使用 await 关键字来等待异步操作完成，并将执行结果存储在 executedContext 变量中。

                ResourceExecutedContext executedContext = await next.Invoke(); //异步调用资源执行的下一个步骤

                /* ResourceExecutingContext：

                表示资源执行之前的上下文信息。
                包含有关请求和资源的信息，如HTTP上下文、路由数据、Action参数等。
                通常在资源执行之前进行操作，例如验证请求、修改请求参数或记录日志等。
                可以通过实现 IResourceFilter 接口的 OnResourceExecuting 方法来访问和处理该上下文。

                ResourceExecutedContext：

                表示资源执行完成后的上下文信息。
                包含有关执行结果和请求的信息，如执行结果、HTTP上下文、路由数据等。
                通常在资源执行完成后进行操作，例如处理执行结果、修改结果、存储缓存等。
                可以通过实现 IResourceFilter 接口的 OnResourceExecuted 方法来访问和处理该上下文。*/

                /*
                    ResouceFilter 的特点，适合什么场景应用呢？---- ResouceFilter 天生就是为了缓存而生的。
                    缓存：就是一个临时存储区域，以一个Key-value格式保存数据；
                    key---保存数据的标识，也需要这个标识key才能获取缓存。
                    请求来了---在还没有做业务逻辑计算之前---判断缓存是否存在，如果存在，就直接返回缓存的值。
                    如果不存在，就应该去做计算，计算完毕，把结果保存到缓存中去；
                    缓存：如果缓存区域中的值没有变化，且key不变的，获取的值就应该是之前的值；
                    url 作为key---- url 不变，缓存就应该不变；
                 */
                CacheDictionary[key] = executedContext.Result; 
            } 
        }
    }
}
