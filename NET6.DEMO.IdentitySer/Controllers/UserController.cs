using Microsoft.AspNetCore.Mvc;
using NET6.Demo.IdentitySer;
using NET6.Demo.IdentitySer.Utility;

namespace NET6.DEMO.IdentitySer.Controllers
{
    //���ܽ���token https://jwt.io/

    [ApiController]
    [Route("[controller]")] // �������������ΪUser
    public class UserController : ControllerBase
    {

        private readonly ICustomJWTService _ICustomJWTService; // Ҳ����������ע��
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, ICustomJWTService iCustomJWTService)
        {
            _logger = logger;
            _ICustomJWTService = iCustomJWTService;
        }

        [HttpGet]
        public IActionResult Login(string name, string password)
        {
            //��仰����У���û���������--�����ڿ����У���ȻҪȥ���ݿ���ȥУ��
            if ("FFFF".Equals(name) && "123456".Equals(password))
            {
                //�����ݿ��в�ѯ������
                var user = new CurrentUser()
                {
                    Id = 123,
                    Name = "FFFF",
                    Age = 36,
                    NikeName = "����",
                    Description = ".NETWebApi������Ա",
                    RoleList = new List<string>() {
                        "admin",
                        "teacher",
                        "student"
                    }
                };
                //��Ӧ������Token 
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
