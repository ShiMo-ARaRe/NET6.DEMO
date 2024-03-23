//这里的代码是用于配置ASP.NET Core应用程序的HTTP请求处理管道和服务容器。 // 程序的入口

//创建一个应用程序构建器builder，该构建器用于配置和构建应用程序。
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//将控制器服务添加到依赖注入容器中。这样可以使应用程序能够使用ASP.NET Core MVC框架来处理和响应HTTP请求。
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//分别添加ApiExplorer和SwaggerGen服务到容器中。这些服务与Swagger/OpenAPI相关，
//用于生成和提供API文档和描ApiExplorer用于生成API的元数据，而SwaggerGen用于生成Swagger规范和文档。


var app = builder.Build(); //使用构建器builder来构建应用程序实例app。

// Configure the HTTP request pipeline.
// 通过if(app.Environment.IsDevelopment())判断当前环境是否为开发环境。
if (app.Environment.IsDevelopment())
{
    // 如果是开发环境，就会启用Swagger和Swagger UI。
    // 使用app.UseSwagger()和app.UseSwaggerUI()将Swagger中间件添加到请求处理管道中，
    // 以便在浏览器中查看和交互式测试API文档。
    app.UseSwagger();
    app.UseSwaggerUI();
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
