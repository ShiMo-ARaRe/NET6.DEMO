using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NET6.DEMO.WebApi.Utility.Filters
{
    /// <summary>
    /// 自定义ResouceFilter扩展缓存
    /// </summary>
    public class CustomCacheResourceFilterAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// 缓存区域
        /// </summary>
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>(); // 静态字典对象，用于缓存数据
        /// <summary>
        /// 在XX资源之后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //如果能够执行到这里，说明一定已经执行了，控制器的构造函数+一定已经执行了API了；
            //必然也已经得到了计算的结果了；就应该把计算的记过保存到缓存中去；
            string key = context.HttpContext.Request.Path;//Url地址

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


            //context.Result 属性表示资源执行的结果，context.Result 的类型是 IActionResult，这是一个接口，用于表示HTTP请求的结果。
            CacheDictionary[key] = context.Result;
            /*将 context.Result（资源执行的结果）存储在缓存字典中，使用 key 作为键。
            这样，在下次相同的请求到达时，可以通过键快速地检索和获取之前缓存的结果，并避免重复的计算或操作。*/
        }

        //执行顺序：OnResourceExecuting方法-->控制器构造函数-->API方法-->OnResourceExecuted方法

        /// <summary>
        /// 在XX资源之前
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuting(ResourceExecutingContext context) // context用于表示资源执行的上下文信息。
        {
            //在这里就应该检查缓存，如果有就直接返回；
            string key = context.HttpContext.Request.Path;//Url地址
            if (CacheDictionary.ContainsKey(key)) // ContainsKey()方法用于检查字典中是否存在指定的键。
            {
                object oResult = CacheDictionary[key]; // 根据键来取值

                //将 oResult 变量转换为 IActionResult 类型。使用 as 运算符进行转换，
                //如果转换成功，则 result 变量将引用转换后的对象；否则，result 将为 null。
                IActionResult result = oResult as IActionResult; 
                context.Result = result;  //请求处理的过程中的一个短路器，如果给Result赋值了，就不继续往后执行了，如果没有赋值，为null,就继续往后执行；
                /* 如果赋值了结果，即指定了响应内容，那么请求处理将会提前结束，不会继续执行后续的过滤器和资源执行逻辑。
                相反，如果未赋值，即保持为 null，请求处理将会继续往后执行。*/
            }
        }
    }
}
