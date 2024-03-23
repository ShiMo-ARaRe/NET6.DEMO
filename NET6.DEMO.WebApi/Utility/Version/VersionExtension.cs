using Microsoft.AspNetCore.Mvc;

namespace NET6.DEMO.WebApi.Utility.Version
{
    /// <summary>
    /// 配置Api支持版本
    /// </summary>
    public static class VersionExtension
    {
        /// <summary>
        /// 配置Core API文档
        /// </summary>
        /// <param name="builder"></param>
        public static void ApiVersionExt(this WebApplicationBuilder builder)
        {
            // 添加 API 版本支持
            builder.Services.AddApiVersioning(o =>
            {
                // 是否在响应的 header 信息中返回 API 版本信息
                o.ReportApiVersions = true;
                // 默认的 API 版本
                o.DefaultApiVersion = new ApiVersion(1, 0); 
                // 未指定 API 版本时，设置 API 版本为默认的版本
                o.AssumeDefaultVersionWhenUnspecified = true;

                //表示默认API版本是1.0
            });

            // 配置 API 版本信息
            builder.Services.AddVersionedApiExplorer(option =>
            {
                // api 版本分组名称
                option.GroupNameFormat = "'v'VVVV";
                // 未指定 API 版本时，设置 API 版本为默认的版本
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
        }
    }
}
