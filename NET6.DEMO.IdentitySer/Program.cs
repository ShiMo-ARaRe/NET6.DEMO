using NET6.Demo.IdentitySer.Utility;

//加密解密token https://jwt.io/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*  配置-->属性
 builder.Services: builder 是一个WebHostBuilder或HostBuilder实例，用于构建和配置应用程序的主机。
Services属性是WebHostBuilder或HostBuilder的一个成员，它允许我们访问应用程序的依赖注入容器。

Configure<JWTTokenOptions>: 这是一个泛型方法，用于配置特定类型（JWTTokenOptions）的服务。
在这种情况下，我们正在配置JWTTokenOptions类型的服务。

uilder.Configuration: 这是一个表示应用程序配置的对象。它提供了访问应用程序配置值的方法。

GetSection("JWTTokenOptions"): 这是一个从应用程序配置（配置文件是appsettings.json）中获取特定配置部分的方法。
在这里，我们正在获取名为JWTTokenOptions的配置部分。
这是一个将配置值绑定到指定类型的方法。通过传递类型（JWTTokenOptions）和配置部分，
它将配置值与JWTTokenOptions类的属性进行匹配和绑定。

总结就是，这行代码的作用是使用依赖注入容器来配置一个名为JWTTokenOptions的选项类，
该选项类的属性将从应用程序配置的JWTTokenOptions部分中获取相应的配置值。
 */
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));// 配置-->属性
/* 依赖注入
将CustomHSJWTService类注册为ICustomJWTService接口的实现类，并将其添加到依赖注入容器中。
这样，我们可以在应用程序的其地方通过依赖注入获取ICustomJWTService接口的实例，并使用CustomHSJWTService类的功能。
 */
builder.Services.AddTransient<ICustomJWTService, CustomHSJWTService>(); // 对称可逆加密
//builder.Services.AddTransient<ICustomJWTService, CustomRSSJWTervice>(); // 非对称可逆加密

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*  将身份验证服务器和资源服务器部署在同一主机上的不同端口，可以提供以下优势：
    逻辑分离：将身份验证逻辑与资源访问逻辑分开，使得系统的架构更加模块化和可维护。
    安全性：通过将身份验证和资源访问分离到不同的端口，可以实施更严格的网络安全策略，例如限制公共访问、设置防火墙规则等。
    水平扩展：独立部署身份验证服务器和资源服务器，可以根据需要对它们进行独立的扩展，以满足不同的负载需求。
 */

app.UseAuthorization(); //授权    --- 已经得到了用户信息，就可以通过用户信息来判定当前用户是否可以访问当前资源

app.MapControllers();

app.Run();
