using Microsoft.OpenApi.Models; // 引入OpenApi模型
using Swashbuckle.AspNetCore.SwaggerGen; // 引入Swagger生成器

/*
这个类的主要作用是在Swagger UI中添加文件上传的功能。当API的参数类型为IFormCollection时，
它会修改OpenApiOperation的RequestBody，使其能够接收multipart/form-data类型的数据，即文件上传数据。
其中，文件的键名为file，类型为string，格式为binary。这样，用户就可以在Swagger UI中直接上传文件了。
 */


namespace NET6.DEMO.WebApi.Utility.Swagger // 定义命名空间
{
    /// <summary>
    /// 扩展文件上传
    /// </summary>
    public class FileUploadFilter : IOperationFilter // 定义一个名为FileUploadFilter的类，实现IOperationFilter接口
        //IOperationFilter 接口是 Swashbuckle.AspNetCore 库中定义的一个接口，用于定义自定义的操作筛选器。
    {
        /// <summary>
        /// 文件上传筛选
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context) // 定义Apply方法，接收OpenApiOperation和OperationFilterContext两个参数
        {
            const string FileUploadContentType = "multipart/form-data"; // 定义一个常量，表示文件上传的内容类型
            if (operation.RequestBody == null || // 如果RequestBody为空，或者
                !operation.RequestBody.Content.Any(x => // RequestBody的Content中没有任何元素满足条件：键名与文件上传的内容类型相同（忽略大小写）
                x.Key.Equals(FileUploadContentType, StringComparison.InvariantCultureIgnoreCase)))
            {
                return; // 则直接返回，不做任何操作
            }

            if (context.ApiDescription.ParameterDescriptions[0].Type == typeof(IFormCollection)) // 如果API描述的第一个参数的类型是IFormCollection
            {
                operation.RequestBody = new OpenApiRequestBody // 则创建一个新的OpenApiRequestBody
                {
                    Description = "文件上传", // 描述为"文件上传"
                    Content = new Dictionary<string, OpenApiMediaType> // 内容为一个字典，键为字符串，值为OpenApiMediaType
                    {
                        {
                            FileUploadContentType, new OpenApiMediaType // 字典中添加一个元素，键为文件上传的内容类型，值为一个新的OpenApiMediaType
                            {
                                Schema = new OpenApiSchema // OpenApiMediaType的Schema属性为一个新的OpenApiSchema
                                {
                                    Type = "object", // OpenApiSchema的Type属性为"object"
                                    Required = new HashSet<string>{ "file" }, // OpenApiSchema的Required属性为一个HashSet，其中包含一个元素："file"
                                    Properties = new Dictionary<string, OpenApiSchema> // OpenApiSchema的Properties属性为一个字典，键为字符串，值为OpenApiSchema
                                    {
                                        {
                                            "file", new OpenApiSchema() // 字典中添加一个元素，键为"file"，值为一个新的OpenApiSchema
                                            {
                                                Type = "string", // OpenApiSchema的Type属性为"string"
                                                Format = "binary" // OpenApiSchema的Format属性为"binary"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}
