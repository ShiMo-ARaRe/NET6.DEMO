/*  Core WebAPI-Filter
 
 * 什么是 AOP ?
 
    AOP（Aspect Orient Programming） ，面向切面编程, 作为面向对象编程的一种补充, 可以在不破坏
    之前的封装为基础动态增加一些功能；从而让系统更具备扩展性；
    增加一个缓存功能
    增加日志的功能
    既希望不要违背开闭原则，也希望能够增加新的工能；
    在之前的业务逻辑之前增加了逻辑；
    在之前的业务逻辑之后增加了逻辑；

 Core WebAPI 中的 AOP 支持有哪些？

    授权--- Authorize
    资源-- Resource
    异常-- Exception
    方法前后--- Action
    AlwayRunResult
    结果前后--- Result
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET6.DEMO.Interfaces;
using NET6.DEMO.WebApi.Utility.Filters; // 引入过滤器
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// AOP-Filter
    /// </summary>
    [ApiController]
    //[CustomExceptionFilter] //当前控制器下的所有的方法都会生效(控制异常的扩展
    [ApiVersion("2.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class FiltersController : ControllerBase
    {
        private readonly ILogger<FiltersController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public FiltersController(ILogger<FiltersController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        //[CustomResourceFilter] //基本扩展
        //[CustomAsyncResourceFilter] //基本扩展(异步版本）
        //[CustomCacheResourceFilter] //扩展缓存
        [CustomAsyncCacheResourceFilter]//扩展异步版本缓存

        public IActionResult GetUser(int id) // 只要是实现了 IActionResult 的接口的，都可以作为返回值；
        {

            Console.WriteLine("=============业务逻辑处理=================");
            Console.WriteLine("=============业务逻辑处理=================");
            Console.WriteLine("=============业务逻辑处理=================");
            var data = new
            {
                Id = id,
                Name = "FFFF",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            ////IAsyncResourceFilter 
            ////IResourceFilter 
            return new JsonResult(data);    //返回Json对象
        }

        /// <summary>
        /// 测试actionFilter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //[CustomActionFilter]
        //[CustomAsyncActionFilter]
        //[CustomCacheActionFilterAttribute]

        //利用TypeFilter 或 ServiceFilter的话就可以传参数了，也就是说上面的写法不能传参数！

        //[TypeFilter(typeof(CustomLogActionFilterAttribute))]
        [ServiceFilter(typeof(CustomLogActionFilterAttribute))]
        //[CustomExceptionFilter]

        public IActionResult ActionFilterApi(int id)
        // 注意！这里的id是以query参数的形式传递的，并不会导致Url的改变，缓存方式可能不符合你的预期！
        {
            //IActionFilter
            //IAsyncActionFilter 
            //ActionFilterAttribute
            _logger.LogInformation("FiltersController...ActionFilterApi 被执行~~~");
            Console.WriteLine("=============业务逻辑处理=================");
            Console.WriteLine("=============业务逻辑处理=================");
            Console.WriteLine("=============业务逻辑处理=================");
            var data = new
            {
                Id = id,
                Name = "FFFF",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(data);
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpPut]
        //[CustomExceptionFilter]

        /*这是一个自定义的特性类，可能是为了提供更简洁的使用方式而创建的。
        它直接将 CustomAsyncExceptionFilterAttribute 过滤器应用于控制器或动作方法。
        这样，您可以在需要的位置直接使用[CustomAsyncExceptionFilter] 特性，而无需显式指定过滤器类型。*/
        //[CustomAsyncExceptionFilter]

        //利用TypeFilter 或 ServiceFilter的话就可以传参数了，也就是说上面的写法不能传参数！

        /*这是 ASP.NET Core 提供的特性，用于显式指定要应用的过滤器类型。
        通过[TypeFilter] 特性，您可以直接将 CustomAsyncExceptionFilterAttribute 过滤器应用于特定的控制器或动作方法。*/
        //[TypeFilter(typeof(CustomAsyncExceptionFilterAttribute))]

        /*这也是 ASP.NET Core 提供的特性，用于将 CustomAsyncExceptionFilterAttribute 过滤器作为服务过滤器
         应用于控制器或动作方法。通过 [ServiceFilter] 特性，您可以在整个应用程序中注册并自动应用该过滤器。*/
        [ServiceFilter(typeof(CustomAsyncExceptionFilterAttribute))] //需要在IOC容器中注册CustomAsyncExceptionFilterAttribute

        public IActionResult ExceptionFilterApi(int id)
        { 
            //IExceptionFilter
            //IAsyncExceptionFilter 
            {
                int i = 0;
                int y = 23;
                int x = y / i;//尝试除以0  不允许的，必然异常
            }

            var data = new
            {
                Id = id,
                Name = "Richard老师",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(new ApiResult<object>()
            {
                Success = true,
                Message = "Ok",
                Data = data
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult ResultFilterApi(int id)
        { 
            var data = new
            {
                Id = id,
                Name = "Richard老师",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(new ApiResult<object>()
            {
                Success = true,
                Message = "Ok",
                Data = data
            });
        }

        // 下面的ResultFilter和AlwaysRunResultFilter 用的比较少

        /// <summary>
        /// 测试ResultFilter/AlwaysRunResultFilter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ResultResultFilterApi")]
        //[CustomResultFilter]
        //[CustomAsyncResultFilter]
        [CustomAlwayRunResultFilter]
        public IActionResult ResultResultFilterApi(int id)
        { 

            var data = new
            {
                Id = id,
                Name = "Richard老师",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(new ApiResult<object>()
            {
                Success = true,
                Message = "Ok",
                Data = data
            });
        }
    }
}