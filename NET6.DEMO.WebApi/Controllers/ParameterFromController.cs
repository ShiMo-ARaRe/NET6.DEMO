//引入了 ASP.NET Core MVC 的命名空间，其中包含了用于创建 Web 应用程序的控制器、动作结果、过滤器等类和特性。
using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.Interfaces; // 引入接口

namespace NET6.DEMO.WebApi.Controllers
{

    /// <summary>
    /// 参数来源，参数多种修饰特性的核心价值
    /// 1  FromServices 
    /// 2  FromBody 
    /// 3  FromForm 
    /// 4  FromHeader 
    /// 5  FromQuery 
    /// 6  FromRoute
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class ParameterFromController : ControllerBase
    {
        private readonly ILogger<ParameterFromController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public ParameterFromController(ILogger<ParameterFromController> logger)
        {
            _logger = logger;
        }

        //为了提高性能，怎么说？
        //如果把ITestServiceB写到构造函数里，那么这个控制器中每个API请求调用时都会去初始化ITestServiceB，
        //就算ITestServiceB用不上，也会初始化，浪费性能!

        #region FromServices 
        /// <summary>
        /// FromServices修饰
        /// 1.表示来自于IOC容器创建
        /// 2.必然需要IOC容器先注册
        /// 
        /// 如果没有标记 [FromServices]，默认会认定这个参数是要通过调用方传递
        /// 
        /// 控制器不是已经有了构造函数注入吗？为什么方法中还提供[FromServices]特性在方法的参数中来获取服务实例呢？ 为了提高性能~
        /// 
        /// </summary>
        /// <param name="ITestServiceB"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromServicesMethod")]
        public string FromServicesMethod([FromServices]ITestServiceB ITestServiceB)
        {
            // 看控制台打印，先构造ITestServiceA，再构造ITestServiceB
            return ITestServiceB.ShowB();
        }

        #endregion

        //配合浏览器F12中Network食用更佳

        //api 搜集来自于客户端请求的参数中，从HTTP Body中去搜集这个参数的数据，通常用于取 JSON,XML,收集到以后,绑定到当前的参数/对象中；
        #region FromBody修饰 

        /// <summary>
        /// FromBody修饰-Get请求---不能访问
        /// </summary>
        /// <param name="user"></param> //param是参数的意思
        /// <returns></returns>
        [HttpGet()]
        [Route("FromBodyMethodGet")]
        public User FromBodyMethodGet([FromBody] User user)
        {
            return user;
        }

        /// <summary>
        /// FromBody修饰-Post请求---可以访问
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromBodyMethodPost")]
        public User FromBodyMethodPost([FromBody] User user)
        {
            return user;
        }

        /// <summary>
        /// FromBody修饰-Put请求---可以访问
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromBodyMethodPut")]
        public User FromBodyMethodPut([FromBody] User user)
        {
            return user;
        }

        /// <summary>
        /// FromBody修饰-Delete请求---可以访问
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromBodyMethodDelete")]
        public User FromBodyMethodDelete([FromBody] User user)
        {
            return user;
        }

        #endregion

        //api 搜集来自于客户端请求的参数中，到Form表单中去搜集这个参数的数据，收集到以后，绑定到当前的参数/对象中；
        #region FromForm修饰

        /// <summary>
        /// FromForm修饰-Get请求---不能访问
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromFormMethodGet")]
        public User FromFormMethodGet([FromForm] User user)
        {
            return user;
        }

        /// <summary>
        /// FromForm修饰-Post请求---可以访问
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromFormMethodPost")]
        public User FromFormMethodPost([FromForm] User user)
        {
            return user;
        }

        /// <summary>
        /// FromForm修饰-Put请求---可以访问
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromFormMethodPut")]
        public User FromFormMethodPut([FromForm] User user)
        {
            return user;
        }

        /// <summary>
        /// FromForm修饰-Delete请求---可以访问
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromFormMethodDelete")]
        public User FromFormMethodDelete([FromForm] User user)
        {
            return user;
        }

        #endregion

        //api 搜集来自于客户端请求的参数中，到Header头信息中去搜集这个参数的数据，收集到以后，绑定到当前的参数/对象中；
        #region FromHeader


        /// <summary>
        /// FromHeader修饰-Get请求---可以访问
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromHeaderMethodGet")]
        public string FromHeaderMethodGet([FromHeader] string header)
        {
            return header;
        }

        /// <summary>
        /// FromHeader修饰-Post请求---可以访问
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromHeaderMethodPost")]
        public string FromHeaderMethodPost([FromHeader] string header)
        {
            return header;
        }

        /// <summary>
        /// FromHeader修饰-Put请求---可以访问
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromHeaderMethodPut")]
        public string FromHeaderMethodPut([FromHeader] string header)
        {
            return header;
        }

        /// <summary>
        /// FromHeader修饰-Delete请求---可以访问
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromHeaderMethodDelete")]
        public string FromHeaderMethodDelete([FromHeader] string header)
        {
            return header;
        }

        #endregion

        //如果客户端通过查询字符串方式传递参数， FromQuery 就是在 Url 地址中去获取值api 搜集来自于客户端请求的参数中，
        //通过URL Query中去搜集这个参数的数据，收集到以后，绑定到当前的参数/对象中；
        #region FromQuery修饰

        /// <summary>
        /// FromQuery修饰-Get请求
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromQueryMethodGet")]
        public string FromQueryMethodGet([FromQuery] string query) // 参数的名字也很重要！
        {
            return query;
        }

        /// <summary>
        /// FromQuery修饰-Post请求
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromQueryMethodPost")]
        public string FromQueryMethodPost([FromQuery] string query)
        {
            return query;
        }

        /// <summary>
        /// FromQuery修饰-Put请求
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromQueryMethodPut")]
        public string FromQueryMethodPut([FromQuery] string query)
        {
            return query;
        }

        /// <summary>
        /// FromQuery修饰-Delete请求
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromQueryMethodDelete")]
        public string FromQueryMethodDelete([FromQuery] string query)
        {
            return query;
        }

        #endregion

        //api 搜集来自于客户端请求的参数中，在路由中去搜集这个参数的数据，收集到以后，绑定到当前的参数/对象中；
        #region FromRoute修饰
         
        /// <summary>
        ///  FromRoute修饰--Get请求--
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromRouteMethodGet/{route}")]
        public string FromRouteMethodGet([FromRoute] string route,string text) // 此时必须收集两个参数，一个通过路由，一个通过查询参数
        {
            return route;
        }

        /// <summary>
        /// FromRoute修饰--Post请求
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromRouteMethodPost/{route}")]
        public string FromRouteMethodPost([FromRoute] string route)
        {
            return route;
        }

        /// <summary>
        /// FromRoute修饰--Put请求
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromRouteMethodPut/{route}")]
        public string FromRouteMethodPut([FromRoute] string route)
        {
            return route;
        }
         
        /// <summary>
        /// FromRoute修饰--Delete请求
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromRouteMethodDelete/{route}")]
        public string FromRouteMethodDelete([FromRoute] string route)
        {
            return route;
        }
        #endregion 
    }
}