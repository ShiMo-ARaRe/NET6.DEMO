//这里的代码是用于配置ASP.NET Core应用程序的HTTP请求处理管道和服务容器。 // 程序的入口

//using static 是 C# 6.0 引入的一个语法，它允许你在代码中直接使用类的静态成员，而无需使用类名作为限定符。
using static System.Net.Mime.MediaTypeNames; //是 .NET Framework 提供的一个类
//它包含了一系列常用的媒体类型名称（media type names）。这些媒体类型名称用于指定各种互联网媒体类型，
//例如文本、图像、音频、视频等。通过使用 MediaTypeNames 类中的静态成员，你可以直接引用这些媒体类型名称，而无需手动输入字符串。

//命名空间提供了一组用于处理和控制进程、事件日志和性能计数器的类。
using System.Diagnostics;
using NET6.DEMO.WebApi.Utility.Swagger; // 版本控制，定义版本号
using Microsoft.OpenApi.Models; // 它包含了用于表示和构建 OpenAPI 规范的类和接口。

using System.Text.Encodings.Web;
using System.Text.Unicode; // 这两个命名空间提供了与编码和 Unicode 相关的功能。// 引入目的是为了解决中文乱码问题

//创建一个应用程序构建器builder，该构建器用于配置和构建应用程序。
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//将控制器服务添加到依赖注入容器中。这样可以使应用程序能够使用ASP.NET Core MVC框架来处理和响应HTTP请求。

builder.Services.AddControllers().AddJsonOptions(options => // 解决中文乱码问题
{
    //可以对 JSON 序列化选项进行自定义配置。这个方法接受一个 Lambda 表达式，用于指定如何配置 JSON 选项。
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//分别添加ApiExplorer和SwaggerGen服务到容器中。这些服务与Swagger/OpenAPI相关，
//用于生成和提供API文档和描ApiExplorer用于生成API的元数据，而SwaggerGen用于生成Swagger规范和文档。
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    //要启用swagger版本控制要在api控制器或者方法上添加特性[ApiExplorerSettings(GroupName = "版本号")]  我这里是枚举
    typeof(ApiVersions).GetEnumNames().ToList().ForEach(version => // version就是枚举值
    //获取了 ApiVersions 枚举类型的所有枚举名称，并进行迭代处理。
    {
        //对于每个枚举名称（即版本号），使用 option.SwaggerDoc(version, new OpenApiInfo() { ... }) 方法
        //添加了一个 Swagger 文档。在这里，version 是枚举名称，用作文档的标识符。
        option.SwaggerDoc(version, new OpenApiInfo()
        {
            //指定了文档的标题、版本号、描述等信息。
            Title = $"{version}-版本:Api文档",
            Version = version,
            Description = $"通用版本的CoreApi版本{version}"
        });
    });
});


var app = builder.Build(); //使用构建器builder来构建应用程序实例app。

// Configure the HTTP request pipeline.
// 通过if(app.Environment.IsDevelopment())判断当前环境是否为开发环境。
if (app.Environment.IsDevelopment())
{
    // 如果是开发环境，就会启用Swagger和Swagger UI。
    // 使用app.UseSwagger()和app.UseSwaggerUI()将Swagger中间件添加到请求处理管道中，
    // 以便在浏览器中查看和交互式测试API文档。
    app.UseSwagger();
    //用于配置 Swagger UI 的选项，以便在浏览器中显示 Swagger 文档。
    app.UseSwaggerUI(option =>
    {
        //遍历了枚举名称集合
        //typeof 是 C# 中的运算符，用于获取指定类型的 System.Type 对象。
        //GetEnumNames() 是 System.Enum 类的一个方法，用于返回枚举类型的所有枚举名称的字符串数组。
        foreach (string version in typeof(ApiVersions).GetEnumNames())
        {
            //对于每个枚举名称（即版本号），使用 option.SwaggerEndpoint(...) 方法
            //添加了一个 Swagger 终结点，这个终结点的 URL 是 /swagger/{version}/swagger.json，
            //其中 {version} 使用当前迭代的枚举名称进行替换。
            option.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Api版本：{version}");
        }
    });
}

app.UseHttpsRedirection(); //用于将HTTP请求重定向到HTTPS，增强安全性

app.UseAuthorization(); //用于启用身份验证和授权功能。

app.MapControllers(); //将控制器路由到相应的动作方法。

app.Run(); //运行应用程序，监听和处理传入的HTTP请求。

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
