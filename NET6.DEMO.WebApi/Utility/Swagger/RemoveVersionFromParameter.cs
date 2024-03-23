using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NET6.DEMO.WebApi.Utility.Swagger
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class RemoveVersionFromParameter : IOperationFilter
    //这段代码定义了一个名为 RemoveVersionFromParameter 的类，实现了 IOperationFilter 接口，用于移除请求参数中的版本信息。
    {
        /// <summary>
        /// 扩展
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
            //Apply是 IOperationFilter 接口中的方法，用于应用筛选器逻辑。
        {
            //通过在操作的参数列表中查找名为 "version" 的参数，获取版本参数对象。
            var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "version");
            if (versionParameter != null)
                //如果找到了版本参数对象，则从操作的参数列表中移除该参数。
            {
                operation.Parameters.Remove(versionParameter);
            } 
        }
    } 
}
