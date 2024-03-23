using Microsoft.AspNetCore.Mvc; //这是一个命名空间引用，表示代码中使用了ASP.NET Core的MVC框架。
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    //这是一个特性标记，应用于控制器类。它指示控制器是一个Web API控制器，
    //并且将自动应用一些默认的行为，如自动模型验证和HTTP响应的自动推断。
    [ApiController]

    //这是一个特性标记，应用于控制器类。它指示控制器的路由模板，其中[controller]将被替换为控制器的名称，
    //即"WeatherForecast"。这意味着该控制器的路由将以"/WeatherForecast"开始。
    [Route("[controller]")]

    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V3))]

    //ControllerBase是ASP.NET Core中控制器的基类。
    public class WeatherForecastController : ControllerBase
    {
        //这是一个私有静态只读数组，包含一组天气摘要字符串。
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //这是一个只读私有字段，表示控制器的日志记录器。它使用泛型ILogger<T>，其中T是WeatherForecastController，
        //以便进行类型安全的日志记录。
        private readonly ILogger<WeatherForecastController> _logger;
        //ILogger<WeatherForecastController> 是一个泛型接口类型，用于在.NET Core应用程序中进行日志记录。

        //这是一个控制器的构造函数，接受一个ILogger<WeatherForecastController>类型的参数。
        //构造函数用于将日志记录器注入到控制器中。
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //这是一个特性标记，应用于Get()方法。它指示该方法将处理HTTP GET请求，
        //并且路由名称为"GetWeatherForecast"。
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //Range这是一个 LINQ 查询，用于生成一个整数序列，从 1 开始，到 5 结束（包括 1 和 5）
            //Select应用于前面生成的整数序列。对于每个整数 index，
            //它执行一个操作并返回一个 WeatherForecast 对象。
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                //这是设置 WeatherForecast 对象的 Date 属性，将当前日期加上 index 天。
                Date = DateTime.Now.AddDays(index),

                //这是设置 WeatherForecast 对象的 TemperatureC 属性，
                //生成一个介于 - 20 和 55 之间的随机整数。
                TemperatureC = Random.Shared.Next(-20, 55),

                //这是设置 WeatherForecast 对象的 Summary 属性，
                //从 Summaries 数组中随机选择一个摘要字符串。
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            //这是将 LINQ 查询结果转换为 WeatherForecast 对象的数组形式，并作为方法的最终返回结果。
        }
        //IEnumerable<WeatherForecast> 是一个泛型接口类型，
        //表示一个可以迭代的、包含多个 WeatherForecast 对象的集合。

        //与使用数组（WeatherForecast[]）相比，使用 IEnumerable<WeatherForecast> 可能会稍微增加一些性能开销和编码复杂性。
        //这是因为 IEnumerable<WeatherForecast> 需要进行迭代器的动态调用，而数组访问的性能更高。
        //因此，在性能要求较高的场景中，可以考虑使用数组或其他更具体的集合类型。
    }
}
