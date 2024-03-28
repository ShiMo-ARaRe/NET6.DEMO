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

using Microsoft.AspNetCore.Authorization;// 该命名空间包含一些用于进行身份验证和授权的类和接口。
using NET6.Demo.WebApi;// 为了方便拿JWTTokenOptions类
//用于处理 JWT（JSON Web Token）身份验证的一个包。该包提供了用于配置和处理 JWT 身份验证的相关类和中间件。
using Microsoft.AspNetCore.Authentication.JwtBearer;
/*提供了在 ASP.NET Core 和 .NET 应用程序中处理安全令牌和加密密钥的功能。
它包含了一些类和方法，用于生成、验证和处理安全令牌以及管理加密密钥。*/
using Microsoft.IdentityModel.Tokens;
using System.Text; // 提供了用于处理文本编码和字符串操作的类和方法。
using System.Security.Claims; // 用于处理身份验证和授权领域的声明（Claims）。
using NET6.Demo.WebApi.Utility;  //放置实用工具类，获得一些常用的功能和功能扩展，以提高开发效率和代码的可维护性。


// 读取NLog配置文件
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("CfgFile/NLog.config").GetCurrentClassLogger();

//创建一个应用程序构建器builder，该构建器用于配置和构建应用程序。
WebApplicationBuilder builder = WebApplication.CreateBuilder(args); // 用var自动推断类型也行

//配置log4net (需要引入 log4net + Microsoft.Extensions.Logging.Log4Net.AspNetCore包
//builder.Logging.AddLog4Net("CfgFile/log4net.Config"); // 使用后会替换掉内置的日志

#region NLog配置  (需要引入NLog.Web.AspNetCore包
/*  ClearProviders() 方法用于清除应用程序的日志提供程序，以确保在配置新的日志
    提供程序之前不会有其他日志记录器被添加。这样可以避免与默认的日志提供程序冲突或重复日志记录的问题。*/
//builder.Logging.ClearProviders();

/*  SetMinimumLevel() 方法用于设置日志记录的最小级别。在这里，将日志记录级别设置为 Information，
    表示只记录信息级别及更高级别的日志消息。更低级别的日志消息（例如 Debug）将被忽略。
    这有助于控制日志的详细程度，以避免记录过多的冗余信息。*/
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);

/*  UseNLog() 方法是使用 NLog 日志库来配置应用程序的日志记录。
    它将 NLog 日志提供程序添加到应用程序的日志记录器中，以便使用 NLog 进行日志记录。*/
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
builder.AddSwaggerGenExt(); // 为了节省Program中的空间，这里的逻辑都写到SwaggerExtension中去了(面向对象中封装的思想

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

/* 在ASP.NET Core的依赖注入容器中注册一个服务。
 * 它告诉容器，当需要IAuthorizationHandler接口的实例时，使用NikeNameAuthorizationHandler类进行创建。
 * 这意味着NikeNameAuthorizationHandler将用于处理授权逻辑。*/
builder.Services.AddTransient<IAuthorizationHandler, NikeNameAuthorizationHandler>();
#endregion

#region 鉴权和授权
// 需要安装Microsoft.AspNetCore.Authentication.JwtBearer包

JWTTokenOptions tokenOptions = new JWTTokenOptions(); // 创建实例
// 将配置文件中的 "JWTTokenOptions" 节点的值绑定到 tokenOptions 对象。
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
builder.Services
  .AddAuthorization(option =>
  //通过builder.Services.AddAuthorization方法将授权服务添加到应用程序的服务容器中。
  {
      /*    角色有
           "admin",
           "teacher",
           "student" */

      //ContainsNickname 策略名称
      option.AddPolicy("Policy001", policyBuilder =>
      //使用option.AddPolicy方法来定义一个名为"Policy001"的授权策略。这个授权策略包含了多个授权要求。
      {
          //必须包含什么
          //授权策略要求用户必须具有"admin"角色、用户名为"FFFF"，并且必须具有名为"NickName"的声明（Claim）。
          policyBuilder.RequireRole("admin");
          policyBuilder.RequireUserName("FFFF");
          policyBuilder.RequireClaim("NickName");

          /*  如果属性中的基本要求验证失败（例如用户没有 "admin" 角色、用户名不是 "FFFF" 或
              没有名为 "NickName" 的声明），请求将被拒绝。*/

          policyBuilder.RequireAssertion(context =>
          /* 使用policyBuilder.RequireAssertion方法来定义一个自定义的授权断言。授权断言是一个委托，
          它接收一个AuthorizationHandlerContext对象（封装了授权过程中所需的上下文信息）作为参数，
          并返回一个布尔值表示授权是否通过。*/
          {
              //在这里可以做很多的逻辑判断
              //用户必须具有名为"admin"的角色声明。
              //用户的第一个角色声明的值必须为"admin"。
              //用户必须具有任何类型为"ClaimTypes.Name"的声明。
              bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role) // 一真为真
                 && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "admin" // 不用Equals方法用 == 也行
                 && context.User.Claims.Any(c => c.Type == ClaimTypes.Name); // 一真为真

              /* 这些方法都会迭代，因为claims的类型是List<Claim>，我们之前把一个用户的信息拆分成了多份！
                 详情见CustomHSJWTService.cs文件中 准备有效载荷 部分*/

              /* 属性与声明
                 属性和声明是密切相关的概念。属性描述用户的特征，而声明用于在身份验证和授权过程中携带和验证这些属性信息。
                 声明的类型属性对应于属性的类型，值属性对应于属性的具体值。
                 属性更常用于通用的编程概念和对象描述，而声明更常用于身份验证和授权的特定场景中，用于携带和验证用户的属性信息。
               */

              /* 关于c.Type
                c.Type 表示声明的类型属性（Type），它指定了声明的含义或类别。
              在身份验证和授权的上下文中，声明类型用于区分不同类型的声明，例如角色、姓名、年龄等。

                Claim 对象通常具有多个属性，其中 Type 属性代表声明的类型，而 Value 属性代表声明的具体值。
              通过访问 c.Type 属性，我们可以获取声明对象 c 的类型属性的值。(注意不是属性值，而是属性的值，即这个属性叫什么！

                在这个特定的表达式中，我们将声明对象 c 的类型属性与 ClaimTypes.Role 进行比较。
              ClaimTypes.Role 是 System.Security.Claims.ClaimTypes 类中定义的一个常量，表示角色类型的声明。*/

              /* HasClaim和Any与First
                HasClaim 是 User 对象的方法，直接在 User 对象上调用，而 Claims.Any 是 Claims 属性的方法，
                需要先访问 User.Claims 获取用户的声明集合，然后调用 Any 方法。
                返回值：都是 bool 类型。如果用户的声明集合中存在至少一个满足条件的声明，则返回 true；否则返回 false。
                它们的返回值在语义上是等价的，用哪个看使用场景和个人喜好。

                User.Claims.First 是用于获取用户声明集合中第一个满足指定条件的声明的方法。
                先通过.First(c => c.Type.Equals(ClaimTypes.Role))拿到属性/声明，再通过.Value取值
                返回值：返回第一个满足条件的声明对象。
               */

              return bResult;
          });

      });

      option.AddPolicy("Policy002", policyBuilder =>
      {
          policyBuilder.AddRequirements(new CustomNickNameRequirement()); //策略授权扩展，把代码逻辑放到其他文件中
      });


      //option.AddPolicy("Policy002", policyBuilder =>
      //{
      //    //必须包含什么
      //    policyBuilder.RequireRole("admin");
      //    policyBuilder.RequireUserName("FFFF");
      //    policyBuilder.RequireClaim("NickName");

      //    policyBuilder.RequireAssertion(context =>
      //    /*    context 是一个 AuthorizationHandlerContext 对象，它封装了授权过程中所需的上下文信息。
      //          AuthorizationHandlerContext 提供了访问用户信息、资源信息以及其他授权相关的数据的能力。
      //          context.User 是 AuthorizationHandlerContext 对象的一个属性，它表示当前请求的用户身份信息。
      //          context.User 是 ClaimsPrincipal 类型的实例，它代表了当前经过身份验证的用户。
      //          context.User.Claims 表示当前经过身份验证的用户所具有的声明集合。
      //          你可以使用 Claims 属性来访问和操作这些声明，例如检查特定类型的声明是否存在、获取声明的值等。
      //     */
      //    {
      //        /* HasClaim 方法接受一个谓词（Predicate）作为参数，用于指定需要满足的声明条件。
      //         * 在这里，谓词 c => c.Type == ClaimTypes.Role 表示要求声明的类型（Type）为 "Role"。
      //         * 如果用户具有 至少一个类型 为 "Role" 的声明，则该条件返回 true。
      //         */
      //        bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role)
      //        /* First 方法用于获取第一个声明。然后，通过 Value 属性获取该声明的值进行比较。
      //           如果第一个角色声明的值等于 "admin"，则该条件返回 true。*/
      //           && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "admin"
      //           && context.User.Claims.Any(c => c.Type == ClaimTypes.Name);
      //        return bResult;
      //        /*  变量 c 在三个不同的位置使用，但其含义是相同的。
      //            用户声明（Claim）的临时变量，用于在 LINQ 查询中进行条件匹配和值获取。*/
      //    });

      //});
  }) //启用授权
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  /* 并将其配置为使用 JWT Bearer 身份验证方案（JwtBearerDefaults.AuthenticationScheme）。
   * 接下来，使用 .AddJwtBearer 方法来配置 JWT Bearer 身份验证的选项和逻辑。*/
    .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
    {
        //在.AddJwtBearer 方法中，我们通过 options.TokenValidationParameters 属性来配置 JWT 身份验证的参数。
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //这里的JWT（JSON Web Token）你就看作是Token就行了

            //JWT有一些默认的属性，就是给鉴权时就可以筛选了

            //如果设置为true，则会验证 JWT 的 Issuer 是否与 ValidIssuer属性 中指定的值匹配。
            ValidateIssuer = true,//指定是否验证 JWT 中的 Issuer（签发者）。
            //如果设置为true，则会验证 JWT 的 Audience 是否与 ValidAudience属性 中指定的值匹配。
            ValidateAudience = true,//指定是否验证 JWT 中的 Audience（受众）。

            ValidateLifetime = true,//是否验证失效时间
            //如果设置为true，系统会自动检测并验证，需要与IssuerSigningKey属性相关联。
            ValidateIssuerSigningKey = true,//指定是否验证 JWT 中的加密密钥。
            /*如果没有指定IssuerSigningKey，即使ValidateIssuerSigningKey设置为true，
             *验证仍然会失败，并且ValidateIssuerSigningKey属性将无效。*/

            ValidAudience = tokenOptions.Audience,//指定有效的 Audience 值，用于与 JWT 中的 Audience 进行比较验证。
            ValidIssuer = tokenOptions.Issuer,//指定有效的 Issuer 值，用于与 JWT 中的 Issuer 进行比较验证。

            // 指定用于验证 JWT 的对称加密密钥。
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),

            //AudienceValidator方法和LifetimeValidator方法是用于自定义验证 JSON Web Token(JWT) 中的参数的委托方法。

            //在这个委托方法中，你可以编写自己的验证逻辑来进一步验证 JWT 的 Audience。
            AudienceValidator = (m, n, z) =>
            //委托的参数(m, n, z)分别表示 JWT 中的 Audience 值、当前的安全令牌(token)、以及验证参数。
            {

                Console.WriteLine($"###### {m.FirstOrDefault()}");
                Console.WriteLine($"###### { m != null && m.FirstOrDefault().Equals("http://localhost:5200")}");
                //这里可以写自己定义的验证逻辑
                return m != null && m.FirstOrDefault().Equals("http://localhost:5200");
                //要求 Audience 值不为null，并且第一个元素与预期的 Audience 值相等时，返回true表示验证通过

                //return true;
            },
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            /*  notBefore 表示 JWT 的生效时间（即在此时间之前 JWT 是无效的），
                expires 表示 JWT 的过期时间（即在此时间之后 JWT 是无效的），
                securityToken 表示要验证的安全令牌(token)
                validationParameters 是验证参数。*/
            {
                // 要求 当前时间DateTime.Now 在JWT的过期时间expires 之前 才能通过验证
                Console.WriteLine($"###### 当前时间{DateTime.Now} ### 过期时间{expires} #########"); ;
                Console.WriteLine($"###### {expires >= DateTime.Now}");

                return expires >= DateTime.Now; // DateTime类型数据可以比较大小
                ////&& validationParameters

                //return true;

            }//自定义校验规则
        };
    });
#endregion

WebApplication app = builder.Build(); //使用构建器builder来构建应用程序实例app。

// Configure the HTTP request pipeline. // 配置HTTP请求管道。
// 通过if(app.Environment.IsDevelopment())判断当前环境是否为开发环境。
if (app.Environment.IsDevelopment())
{
    //对于Swagger中间件的引入
    //app.UseSwaggerExt();  // 为了节省Program中的空间，这里的逻辑都写到SwaggerExtension中去了
}

app.UseSwaggerExt();//让IIS部署CoreWebApi时也启用Swagger

app.UseHttpsRedirection(); //用于将HTTP请求重定向到HTTPS，增强安全性

app.UseAuthentication();//鉴权    ----请求来的时候，把请求中带的token/Session/Cookies做解析，取出用户信息

// 用于启用身份验证和授权功能，包括角色授权和策略授权
app.UseAuthorization(); //授权    --- 已经得到了用户信息，就可以通过用户信息来判定当前用户是否可以访问当前资源

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
