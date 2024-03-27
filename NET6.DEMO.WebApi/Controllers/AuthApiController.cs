using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET6.DEMO.Interfaces;
using NET6.DEMO.WebApi.Utility.Filters;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// AOP-Filter
    /// </summary>
    [ApiController]
    [ApiVersion("3.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class AuthApiController : ControllerBase
    {
        private readonly ILogger<AuthApiController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AuthApiController(ILogger<AuthApiController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //这是一个用于授权和身份验证的特性（Attribute）标记，通常用于控制器或操作方法上。
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        /*该特性指定了使用 JWT Bearer 认证方案进行授权和身份验证。
         * 具体来说，AuthenticationSchemes属性被设置为JwtBearerDefaults.AuthenticationScheme，
         * 它表示使用 JWT Bearer 认证方案进行身份验证。*/

        //场景一：如果要验证多个同时具备，同时具备多个role,就标记多个 Authorize，分别把角色写上,多个角色是并且的关系
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "teacher")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "student")]

        //这种方式是通过角色来进行授权，而不是使用自定义的授权策略。

        //场景二：多个角色，只要有一个角色匹配即可，多个角色为或者的关系,只需要标记一个Authorize特性，roles="角色名称以逗号分割"，逗号分割的角色名称是或者的关系；

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "teacher,admin,student")]
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            var data = new
            {
                Id = id,
                Name = "老王",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(data);
        }

        /// <summary>
        /// 策略授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Policy001")]
        // 使用了 Policy 参数来指定授权策略的名称。
        // 它要求用户在进行授权时必须满足名为 "Policy001" 的授权策略中定义的所有要求
        // 这个授权策略可以在代码中通过 AddAuthorization 方法进行定义
        [Route("GetUserPolicy")]
        [HttpGet]
        public IActionResult GetUserPolicy(int id)
        {
            var data = new
            {
                Id = id,
                Name = "老李",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(data);
        }



        [Route("GetUserPolicyRequirement")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Policy002")]
        [HttpGet]
        public IActionResult GetUserPolicyRequirement(int id)
        {
            var data = new
            {
                Id = id,
                Name = "老赵",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(data);
        }

    }
}