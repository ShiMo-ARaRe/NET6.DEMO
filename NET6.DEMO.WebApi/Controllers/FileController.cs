using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 文件资源
    /// </summary>
    [ApiController]
    [Route("[controller]")]

    //这是一个特性标记，用于指定控制器方法的 API Explorer 设置。
    //[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V2))] // ApiVersions是版本枚举
    //这个属性告诉 API Explorer 不要忽略这个控制器方法。
    //这个属性用于指定控制器方法所属的 API 分组名称。在这个例子中，使用了 ApiVersions.V2，
    //它是一个命名常量，表示 API 的版本号为 V2。通过指定分组名称，
    //可以将控制器方法按照不同的分组进行文档化和显示。


#region 关于nameof
    //nameof 是 C# 中的一个运算符，用于获取指定符号的名称（名称作为字符串）。
    //它在编译时进行求值，返回作为参数传递的标识符的名称。
    //string propertyName = "Name";
    //string name = nameof(propertyName);
    //在上面的代码中，nameof(propertyName) 将返回字符串 "propertyName"。
    //这样，我们可以确保在将属性名称用作字符串时，它与实际属性的名称保持一致，从而避免了手动输入字符串的错误。
#endregion


    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "File")]
        public JsonResult UploadFile(IFormCollection form)
        //IFormCollection 是一个接口，用于表示包含表单数据的集合。
        //JsonResult 是一个类，用于表示将数据序列化为 JSON 格式的 HTTP 响应。

        {
            //通过 return new JsonResult(...) 创建了一个 JsonResult 对象，并将其作为方法的返回值。
            //这个对象将被转换为 JSON 格式，并作为 HTTP 响应发送给客户端。
            //JsonResult 允许将任意对象作为数据进行序列化，并提供了一些属性和方法用于配置 JSON 序列化的行为。
            return new JsonResult(new
            {
                Success = true,
                Message = "上传成功",
                FileName = form.Files.FirstOrDefault()?.FileName // ?表示表示为null
                //获取上传文件的文件名。form.Files 是一个集合，包含了上传的文件对象。
                //FirstOrDefault() 方法用于获取第一个文件对象，然后通过.FileName 属性获取文件名。
            });
            //返回一个 JSON 响应。这个 JSON 包含了三个属性：Success 属性表示上传是否成功，
            //Message 属性是一个消息字符串，表示上传结果，FileName 属性包含了上传文件的文件名。
        }
        //这个 UploadFile 方法在提供了一个 JSON 响应后，并没有将上传的文件存储到本地。
        //这个方法仅仅返回了上传文件的相关信息作为响应。

    }
}