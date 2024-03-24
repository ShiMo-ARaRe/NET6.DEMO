using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 用户资源
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V1))]
    [Route("[controller]/v{version:apiVersion}")]
    //[Route("[controller]/[action]")] // 不推荐这样使用，因为它违反RESTful风格。[action]暴露了方法名，不安全
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        //[Route("GetUser")] // 路由可以单独配置到方法里，并会与控制器配置的路由拼接到一起
        public User GetUser()
        {
            return new()
            {
                Id = 123,
                Name = "Richard---来自于1.0版本的数据",
                Age = 28
            };
        }
         
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        public int AddUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        public int UpdateUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete()]
        public int DeleteUser(int userId)
        {
            return 1;
        }
    }
}