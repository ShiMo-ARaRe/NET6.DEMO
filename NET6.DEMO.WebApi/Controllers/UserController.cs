using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// �û���Դ
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V1))]
    [Route("[controller]/v{version:apiVersion}")]
    //[Route("[controller]/[action]")] // ���Ƽ�����ʹ�ã���Ϊ��Υ��RESTful���[action]��¶�˷�����������ȫ
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        //[Route("GetUser")] // ·�ɿ��Ե������õ��������������������õ�·��ƴ�ӵ�һ��
        public User GetUser()
        {
            return new()
            {
                Id = 123,
                Name = "Richard---������1.0�汾������",
                Age = 28
            };
        }
         
        /// <summary>
        /// �����û���Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        public int AddUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// �޸��û���Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        public int UpdateUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// ɾ���û���Ϣ
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