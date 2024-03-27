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
        //����һ��������Ȩ�������֤�����ԣ�Attribute����ǣ�ͨ�����ڿ���������������ϡ�
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        /*������ָ����ʹ�� JWT Bearer ��֤����������Ȩ�������֤��
         * ������˵��AuthenticationSchemes���Ա�����ΪJwtBearerDefaults.AuthenticationScheme��
         * ����ʾʹ�� JWT Bearer ��֤�������������֤��*/

        //����һ�����Ҫ��֤���ͬʱ�߱���ͬʱ�߱����role,�ͱ�Ƕ�� Authorize���ֱ�ѽ�ɫд��,�����ɫ�ǲ��ҵĹ�ϵ
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "teacher")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "student")]

        //���ַ�ʽ��ͨ����ɫ��������Ȩ��������ʹ���Զ������Ȩ���ԡ�

        //�������������ɫ��ֻҪ��һ����ɫƥ�伴�ɣ������ɫΪ���ߵĹ�ϵ,ֻ��Ҫ���һ��Authorize���ԣ�roles="��ɫ�����Զ��ŷָ�"�����ŷָ�Ľ�ɫ�����ǻ��ߵĹ�ϵ��

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "teacher,admin,student")]
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            var data = new
            {
                Id = id,
                Name = "����",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(data);
        }

        /// <summary>
        /// ������Ȩ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Policy001")]
        // ʹ���� Policy ������ָ����Ȩ���Ե����ơ�
        // ��Ҫ���û��ڽ�����Ȩʱ����������Ϊ "Policy001" ����Ȩ�����ж��������Ҫ��
        // �����Ȩ���Կ����ڴ�����ͨ�� AddAuthorization �������ж���
        [Route("GetUserPolicy")]
        [HttpGet]
        public IActionResult GetUserPolicy(int id)
        {
            var data = new
            {
                Id = id,
                Name = "����",
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
                Name = "����",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(data);
        }

    }
}