using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NET6.DEMO.WebApi.Utility.Filters
{

    /// <summary>
    /// ActionFilter扩展定制
    /// </summary>
    public class CustomCacheActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 缓存区域
        /// </summary>
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 在XX执行Action执行之后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //如果能够执行到这里，说明一定已经执行了，控制器的构造函数+一定已经执行了API了；
            //必然也已经得到了计算的结果了；就应该把计算的记过保存到缓存中去；
            string key = context.HttpContext.Request.Path;//Url地址
            CacheDictionary[key] = context.Result;
        }

        /// <summary>
        /// 在XXAction执行之前
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        { 
            //在这里就应该检查缓存，如果有就直接返回；
            string key = context.HttpContext.Request.Path;//Url地址   // 注意！不管你id怎么变，Url都不会变，因为query参数不在Url中！
            if (CacheDictionary.ContainsKey(key))
            {
                object oResult = CacheDictionary[key];
                IActionResult result = oResult as IActionResult;
                context.Result = result;  //请求处理的过程中的一个短路器，如果给Result赋值了，就不继续往后执行了，如果没有赋值，为null,就继续往后执行；
            }
        }
    }
}
