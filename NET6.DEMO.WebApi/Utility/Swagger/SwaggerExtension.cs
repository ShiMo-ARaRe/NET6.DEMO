using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NET6.DEMO.WebApi.Utility.Swagger
{
    /// <summary>
    /// Swagger扩展--来一个独立的封装
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Swagger完整配置
        /// </summary>
        public static void AddSwaggerGenExt(this WebApplicationBuilder builder)
        {
            //分别添加ApiExplorer和SwaggerGen服务到容器中。这些服务与Swagger/OpenAPI相关，
            //用于生成和提供API文档和描ApiExplorer用于生成API的元数据，而SwaggerGen用于生成Swagger规范和文档。
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                #region 分版本的Swagger配置
                ////要启用swagger版本控制要在api控制器或者方法上添加特性[ApiExplorerSettings(GroupName = "版本号")]  我这里是枚举
                //typeof(ApiVersions).GetEnumNames().ToList().ForEach(version => // version就是枚举值
                //                                                               //获取了 ApiVersions 枚举类型的所有枚举名称，并进行迭代处理。
                //{

                //    //对于每个枚举名称（即版本号），使用 option.SwaggerDoc(version, new OpenApiInfo() { ... }) 方法
                //    //添加了一个 Swagger 文档。在这里，version 是枚举名称，用作文档的标识符。
                //    option.SwaggerDoc(version, new OpenApiInfo()
                //    {
                //        //指定了文档的标题、版本号、描述等信息。
                //        Title = $"{version}-版本:Api文档",
                //        Version = version,
                //        Description = $"通用版本的CoreApi版本{version}"
                //    });
                //});
                #endregion

                #region 组件支持版本展示
                {
                    // 根据 API 版本信息生成 API 文档 
                    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        option.SwaggerDoc(description.GroupName, new OpenApiInfo
                        {
                            Contact = new OpenApiContact
                            {
                                Name = "FZJ",
                                Email = "2366088044@qq.com"
                            },
                            Description = ".NET6 WebAPI 文档",
                            Title = ".NET6 WebAPI 文档",
                            Version = description.ApiVersion.ToString()
                        });
                    }
                    // 在 Swagger 文档显示的 API 地址中将版本信息参数替换为实际的版本号
                    option.DocInclusionPredicate((version, apiDescription) =>
                    {
                        if (!version.Equals(apiDescription.GroupName))
                            return false;

                        IEnumerable<string> values = apiDescription!.RelativePath
                            .Split('/')
                            .Select(v => v.Replace("v{version}", apiDescription.GroupName));
                        apiDescription.RelativePath = string.Join("/", values);
                        return true;
                    });

                    // 参数使用驼峰命名方式
                    option.DescribeAllParametersInCamelCase();

                    // 取消 API 文档需要输入版本信息，因为Swagger已经能控制/切换版本了，我们没必要再输入版本信息。
                    option.OperationFilter<RemoveVersionFromParameter>(); // 需要实现IOperationFilter接口
                    // 接口实现见RemoveVersionFromParameter.cs文件
                }
                #endregion

                #region 配置展示注释
                {
                    /* xml文档绝对路径 
                    AppContext.BaseDirectory是执行目录/根目录，比如本项目是的执行目录是：
                    D:\C#\NET6-WebApi\NET6.DEMO\NET6.DEMO.WebApi\bin\Debug\net6.0  */
                    var file = Path.Combine(AppContext.BaseDirectory, "NET6.DEMO.WebApi.xml");
                    // file 是 XML 注释文件的路径，true 表示要启用控制器层的注释显示。
                    option.IncludeXmlComments(file, true);
                    // 对action的名称进行排序，如果有多个，就可以看见效果了。
                    option.OrderActionsBy(o => o.RelativePath);
                    /* 在这里，我们使用 o => o.RelativePath 的 lambda 表达式作为参数，表示按照动作的相对路径进行排序。
                      * 这样可以在 Swagger 文档中按照路径的顺序显示 API 动作，使其更加有序和易于查找。*/
                }
                #endregion

                #region 扩展传入Token
                {
                    //添加安全定义
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    //添加安全要求
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference =new OpenApiReference()
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id ="Bearer"
                                }
                            },
                            new string[]{ }
                        }
                     });
                 }
                #endregion

                #region 扩展文件上传按钮
                /*使用 option.OperationFilter<FileUploadFilter>() 方法
                将 FileUploadFilter 类作为操作筛选器应用到 Swagger 选项中。*/
                {
                    option.OperationFilter<FileUploadFilter>();
                }
                #endregion

            });
        }

        /// <summary>
        /// swagger中间件配置应用
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerExt(this WebApplication app)
        {

                // 如果是开发环境，就会启用Swagger和Swagger UI。
                // 使用app.UseSwagger()和app.UseSwaggerUI()将Swagger中间件添加到请求处理管道中，
                // 以便在浏览器中查看和交互式测试API文档。
                app.UseSwagger();
                //用于配置 Swagger UI 的选项，以便在浏览器中显示 Swagger 文档。
                app.UseSwaggerUI(option =>
                {
                    ////遍历了枚举名称集合
                    ////typeof 是 C# 中的运算符，用于获取指定类型的 System.Type 对象。
                    ////GetEnumNames() 是 System.Enum 类的一个方法，用于返回枚举类型的所有枚举名称的字符串数组。
                    //foreach (string version in typeof(ApiVersions).GetEnumNames())
                    //{
                    //    //对于每个枚举名称（即版本号），使用 option.SwaggerEndpoint(...) 方法
                    //    //添加了一个 Swagger 终结点，这个终结点的 URL 是 /swagger/{version}/swagger.json，
                    //    //其中 {version} 使用当前迭代的枚举名称进行替换。
                    //    option.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Api版本：{version}");
                    //}

                    #region 调用第三方程序包支持版本控制
                    {
                        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                        // 默认加载最新版本的 API 文档
                        foreach (var description in provider.ApiVersionDescriptions.Reverse())
                        {
                            option.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                $" API版本 {description.GroupName.ToUpperInvariant()}");
                        }
                    }
                    #endregion
                });

        }
    }
}
