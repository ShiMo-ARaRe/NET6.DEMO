//这里的代码是用于配置ASP.NET Core应用程序的HTTP请求处理管道和服务容器。 // 程序的入口

//using static 是 C# 6.0 引入的一个语法，它允许你在代码中直接使用类的静态成员，而无需使用类名作为限定符。
using static System.Net.Mime.MediaTypeNames; //是 .NET Framework 提供的一个类
//它包含了一系列常用的媒体类型名称（media type names）。这些媒体类型名称用于指定各种互联网媒体类型，
//例如文本、图像、音频、视频等。通过使用 MediaTypeNames 类中的静态成员，你可以直接引用这些媒体类型名称，而无需手动输入字符串。

using System.Diagnostics; //命名空间提供了一组用于处理和控制进程、事件日志和性能计数器的类。
using NET6.DEMO.WebApi.Utility.Swagger; // 版本控制，定义版本号
using Microsoft.OpenApi.Models; // 它包含了用于表示和构建 OpenAPI 规范的类和接口。

using System.Text.Encodings.Web;
using System.Text.Unicode; // 这两个命名空间提供了与编码和 Unicode 相关的功能。// 引入目的是为了解决中文乱码问题

using NET6.DEMO.WebApi.Utility.Route; // 全局路由扩展
using Microsoft.AspNetCore.Mvc; //这是一个命名空间引用，表示代码中使用了ASP.NET Core的MVC框架。

using NET6.DEMO.WebApi.Utility.Version; // 利用包来进行配置Api支持版本

//具备抽象【接口和抽象类】和实现【普通类】
using NET6.DEMO.Interfaces; // 抽象
using NET6.DEMO.Services; // 具体

using NLog.Web;
using NLog; // 利用NLog包来管理日志

using NET6.DEMO.WebApi.Utility.Filters; // 引入过滤器
using Microsoft.AspNetCore.Authorization; 

// 读取NLog配置文件
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("CfgFile/NLog.config").GetCurrentClassLogger();

//创建一个应用程序构建器builder，该构建器用于配置和构建应用程序。
WebApplicationBuilder builder = WebApplication.CreateBuilder(args); // 用var自动推断类型也行

//配置log4net (需要引入 log4net + Microsoft.Extensions.Logging.Log4Net.AspNetCore包
//builder.Logging.AddLog4Net("CfgFile/log4net.Config"); // 使用后会替换掉内置的日志

#region NLog配置  (需要引入NLog.Web.AspNetCore包

//builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Host.UseNLog();

#endregion

// Add services to the container.// 将服务添加到容器中。
//将控制器服务添加到依赖注入容器中。这样可以使应用程序能够使用ASP.NET Core MVC框架来处理和响应HTTP请求。
builder.Services.AddControllers(option => // 全局配置
{
    //option.Filters.Add<CustomExceptionFilterAttribute>(); //全局注册 --对于整个项目中所有的方法都生效的(控制异常的扩展

    //RouteAttribute 是 ASP.NET Core 中的一个特性（Attribute），它用于指定控制器或操作的路由模板。
    //特性可以应用于控制器类或控制器中的操作方法，用于定义它们的路由路径。
    option.Conventions.Insert(0, new RouteConvention(new RouteAttribute("api/"))); // 给所有控制器添加路由前缀
    // 第一个参数必须为0（不填0就会报错），表示添加到最前面。
}
).AddJsonOptions(options => // 解决中文乱码问题
{
    //可以对 JSON 序列化选项进行自定义配置。这个方法接受一个 Lambda 表达式，用于指定如何配置 JSON 选项。
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

// 了解有关配置 Swagger/OpenAPI 的更多信息，请访问 https://aka.ms/aspnetcore/swashbuckle

//利用包来进行配置API版本控制
builder.ApiVersionExt(); // 使用这个的优势是，不同API版本的控制器名字可以一样，但是也要注意，它们必须处于不同文件夹下！

//关于Swagger的完整配置
builder.AddSwaggerGenExt(); // 为了节省Program中的空间，这里的逻辑都写到SwaggerExtension中去了，面向对象中封装的思想

#region 注册抽象和具体之间的关系
//这段代码使用了依赖注入容器（通常是 ASP.NET Core 中的内置容器）的 Services 属性来注册服务的抽象和具体之间的关系。
    builder.Services.AddTransient<ITestServiceA, TestServiceA>();
//AddTransient 方法用于将服务注册为瞬态（Transient）生命周期。
//瞬态生命周期表示每次请求服务（这个请求服务意思就是，当我们试图实例化接口时）时都会创建一个新的TestServiceA实例。

builder.Services.AddTransient<ITestServiceB, TestServiceB>();
builder.Services.AddTransient<IStudentService, StudentService>();

/* 为了支持ServcieFilter
 如果不注册这两个服务，那么就无法通过[ServiceFilter(typeof(CustomLogActionFilterAttribute))]或
 [ServiceFilter(typeof(CustomLogActionFilterAttribute))]将过滤器应用于控制器或API方法。
 */
builder.Services.AddTransient<CustomAsyncExceptionFilterAttribute>();
builder.Services.AddTransient<CustomLogActionFilterAttribute>();



//builder.Services.AddTransient<IAuthorizationHandler, NikeNameAuthorizationHandler>();
#endregion

WebApplication app = builder.Build(); //使用构建器builder来构建应用程序实例app。

// Configure the HTTP request pipeline. // 配置HTTP请求管道。
// 通过if(app.Environment.IsDevelopment())判断当前环境是否为开发环境。
if (app.Environment.IsDevelopment())
{
    //对于Swagger中间件的引入
    app.UseSwaggerExt();  // 为了节省Program中的空间，这里的逻辑都写到SwaggerExtension中去了
}



app.UseHttpsRedirection(); //用于将HTTP请求重定向到HTTPS，增强安全性

app.UseAuthorization(); //用于启用身份验证和授权功能。

app.MapControllers(); //将控制器路由到相应的动作方法。

app.Run(); //运行应用程序，监听和处理传入的HTTP请求。

//用Microsoft.AspNetCore.Mvc.Versioning包可以实现版本控制，但是已被弃用

//控制台打印：
//info: Microsoft.Hosting.Lifetime[14]
//      Now listening on: https://localhost:7012
//info: Microsoft.Hosting.Lifetime[14]
//      Now listening on: http://localhost:5199
//info: Microsoft.Hosting.Lifetime[0]
//      Application started. Press Ctrl+C to shut down.
//info: Microsoft.Hosting.Lifetime[0]
//      Hosting environment: Development
//info: Microsoft.Hosting.Lifetime[0]
//      Content root path: D:\C#\NET6-WebApi\NET6.DEMO\NET6.DEMO.WebApi\

//上面这些日志信息是来自应用程序的托管生命周期（Microsoft.Hosting.Lifetime）模块。它们提供了有关应用程序的运行状态和配置的信息。

/*
Now listening on: https://localhost:7012：表示应用程序正在侦听位于本地主机（localhost）的端口7012上的HTTPS请求，
即应用程序已成功启动并正在接受安全连接的请求。

这两个端口是应用程序在不同协议（HTTP和HTTPS）下监听的端口，用于接受客户端的请求。

Now listening on: http://localhost:5199：表示应用程序正在侦听位于本地主机（localhost）的端口5199上的HTTP请求，
即应用程序已成功启动并正在接受非安全连接的请求。这可能是在开发环境中，应用程序同时支持HTTP和HTTPS。

Application started. Press Ctrl+C to shut down.：表示应用程序已启动成功。这是一个提示，告知您可以按下Ctrl+C组合键来关闭应用程序。

Hosting environment: Development：表示当前的托管环境为开发环境。这是一个指示，告知您应用程序当前正在以开发环境的配置和设置运行。

Content root path: D:\C#\NET6-WebApi\NET6.DEMO\NET6.DEMO.WebApi\：表示应用程序的内容根路径，即应用程序所在的根文件夹的路径。
在这个例子中，应用程序根文件夹的路径是D:\C#\NET6-WebApi\NET6.DEMO\NET6.DEMO.WebApi\。 

这些日志信息提供了应用程序的关键运行时信息，包括监听的端口、启动状态、托管环境和内容根路径。
它们对于调试和监视应用程序的运行非常有用，并可以帮助您了解应用程序的配置和部署情况。
 */
