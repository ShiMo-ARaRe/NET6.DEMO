using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 特性路由解读
    /// </summary>
    [ApiController] // 如果去掉的话，Swagger就无法展示这个Api
    [Route("[controller]")] // 每个控制器必须都要有这个标记
    public class AttributeRouteController : ControllerBase
    {
        private readonly ILogger<AttributeRouteController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public AttributeRouteController(ILogger<AttributeRouteController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
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
        /// 通过Id获取信息
        /// </summary>
        /// <returns></returns> 
        [HttpGet()]
        [Route("{id:int}")] // 给id约束成int类型，防止用户/前端输入不合法类型
        // 如果不加约束，面对错误输入时，响应400，表示路由匹配成功，但类型错误；加了约束，那就立马响应404，表示路由不合规/匹配失败（非黑即白，节约服务器资源）
        public User GetUserById(int id)
        {
            return new()
            {
                Id = id,
                Name = "Richard---来自于1.0版本的数据",
                Age = 28
            };
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{pageIndex:int}/{pageSize:int}/{keyword?}")] // 通过路由规则进行匹配
        public object GetPageUser(int pageIndex, int pageSize, string? keyword)
        {
            return new
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                keyword = keyword
            };
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        public int AddUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        public int UpdateUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// 删除信息
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