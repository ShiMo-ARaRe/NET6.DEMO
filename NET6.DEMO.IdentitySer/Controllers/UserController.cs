using Microsoft.AspNetCore.Mvc;
using NET6.Demo.IdentitySer;
using NET6.Demo.IdentitySer.Utility;

namespace NET6.DEMO.IdentitySer.Controllers
{
    //加密解密token https://jwt.io/

    [ApiController]
    [Route("[controller]")] // 这个控制器名字为User
    public class UserController : ControllerBase
    {

        private readonly ICustomJWTService _ICustomJWTService; // 也是利用依赖注入
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, ICustomJWTService iCustomJWTService)
        {
            _logger = logger;
            _ICustomJWTService = iCustomJWTService;
        }

        [HttpGet]
        public IActionResult Login(string name, string password)
        {
            //这句话就是校验用户名和密码--你们在开发中，当然要去数据库中去校验
            if ("FFFF".Equals(name) && "123456".Equals(password))
            {
                //从数据库中查询出来的
                var user = new CurrentUser()
                {
                    Id = 123,
                    Name = "FFFF",
                    Age = 36,
                    NikeName = "老王",
                    Description = ".NETWebApi开发人员",
                    RoleList = new List<string>() {
                        "admin",
                        "teacher",
                        "student"
                    }
                };
                //就应该生成Token 
                string token = _ICustomJWTService.GetToken(user);
                return new JsonResult(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return new JsonResult(new
                {
                    result = false,
                    token = ""
                });
            }
        }
    }
}
