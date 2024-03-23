using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers.V2
{
    /// <summary>
    /// 框架提供的版本控制
    /// </summary>
    [ApiController] 
    [ApiVersion("2.0")] 
    [Route("[controller]/v{version:apiVersion}")] // 添加约束，RESTful风格
    public class VersionControlController : ControllerBase
    {
        private readonly ILogger<VersionControlController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public VersionControlController(ILogger<VersionControlController> logger)
        {
            _logger = logger;
        }
         
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public object GetVersion()
        {
            return new 
            {
                Id = 123,
                Name = "版本号2.0---来自于第二版本的响应",
                Age = 28
            };
        }

        /// <summary>
        /// 新增版本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public int AddVersion()
        {
            return 1;
        }

        /// <summary>
        /// 修改版本信息
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public int UpdateVersion()
        {
            return 1;
        }

        /// <summary>
        /// 删除版本信息
        /// </summary>
        /// <param name="VersionId"></param>
        /// <returns></returns>
        [HttpDelete()]
        public int DeleteVersion(int VersionId)
        {
            return 1;
        }
    }
}