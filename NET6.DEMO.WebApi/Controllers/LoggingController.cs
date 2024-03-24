using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.Interfaces;
using NET6.DEMO.WebApi.Utility.Swagger;

//注意：不能允许没有任何监控的系统上线；
//如何监控-- - 日志记录
//需要日志信息的持久化--保存到文件中，保存到数据库中；

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 日志记录
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public IActionResult GetLog()
        {
            // LogError 和 LogInformation 是 .NET Core 中内置的方法，用于记录日志。
            // 这些方法是通过 ASP.NET Core 中的内置日志记录器（ILogger 接口的实现）提供的。
            _logger.LogError("LogError:=========Get Api被调用==========="); //错误级别的日志
            _logger.LogInformation("LogInformation:=========Get Api被调用==========="); //信息级别的日志
            #region 调用后见控制台打印
            // fail: NET6.DEMO.WebApi.Controllers.LoggingController[0]
            // LogError:========= Get Api被调用 ===========
            // info: NET6.DEMO.WebApi.Controllers.LoggingController[0]
            // LogInformation:========= Get Api被调用 ===========
            #endregion

            /* 外部方式
        log4net 日志记录
        支持文本日志，数据库日志
        1、 Nuget 引入程序包 log4net + Microsoft.Extensions.Logging.Log4Net.AspNetCore
        2、准备配置文件【设置为始终复制】 // CfgFile文件夹下log4net.Config文件（右键属性）
            （如果不设置成始终复制，那么该文件可能就不会被编译）
        3、植入 log4net
            builder.Logging.AddLog4Net("CfgFile/log4net.Config");
        4、注入log对象，写日志，写入 txt 文件 */

            return new JsonResult(new ApiResult<string>()
            {
                Success = true,
                Data = "日志记录"
            });
        }

    }
}