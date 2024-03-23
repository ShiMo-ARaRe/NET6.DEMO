using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// ����·�ɽ��
    /// </summary>
    [ApiController] // ���ȥ���Ļ���Swagger���޷�չʾ���Api
    [Route("[controller]")] // ÿ�����������붼Ҫ��������
    public class AttributeRouteController : ControllerBase
    {
        private readonly ILogger<AttributeRouteController> _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger"></param>
        public AttributeRouteController(ILogger<AttributeRouteController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
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
        /// ͨ��Id��ȡ��Ϣ
        /// </summary>
        /// <returns></returns> 
        [HttpGet()]
        [Route("{id:int}")] // ��idԼ����int���ͣ���ֹ�û�/ǰ�����벻�Ϸ�����
        // �������Լ������Դ�������ʱ����Ӧ400����ʾ·��ƥ��ɹ��������ʹ��󣻼���Լ�����Ǿ�������Ӧ404����ʾ·�ɲ��Ϲ�/ƥ��ʧ�ܣ��Ǻڼ��ף���Լ��������Դ��
        public User GetUserById(int id)
        {
            return new()
            {
                Id = id,
                Name = "Richard---������1.0�汾������",
                Age = 28
            };
        }

        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{pageIndex:int}/{pageSize:int}/{keyword?}")] // ͨ��·�ɹ������ƥ��
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
        /// ������Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        public int AddUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// �޸���Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        public int UpdateUser(User user)
        {
            return 1;
        }

        /// <summary>
        /// ɾ����Ϣ
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