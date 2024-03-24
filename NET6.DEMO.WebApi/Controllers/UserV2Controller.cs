using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 用户资源
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    //[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V2))]
    [Route("[controller]/v{version:apiVersion}")]
    public class UserV2Controller : ControllerBase
    {
        private readonly ILogger<UserV2Controller> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public UserV2Controller(ILogger<UserV2Controller> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public User GetUser()
        {
            return new()
            {
                Id = 123,
                Name = "Richard---来自于2.0版本的数据",
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