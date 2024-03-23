using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers.V2
{
    /// <summary>
    /// ����ṩ�İ汾����
    /// </summary>
    [ApiController] 
    [ApiVersion("2.0")] 
    [Route("[controller]/v{version:apiVersion}")] // ���Լ����RESTful���
    public class VersionControlController : ControllerBase
    {
        private readonly ILogger<VersionControlController> _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger"></param>
        public VersionControlController(ILogger<VersionControlController> logger)
        {
            _logger = logger;
        }
         
        /// <summary>
        /// ��ȡ�汾��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public object GetVersion()
        {
            return new 
            {
                Id = 123,
                Name = "�汾��2.0---�����ڵڶ��汾����Ӧ",
                Age = 28
            };
        }

        /// <summary>
        /// �����汾��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public int AddVersion()
        {
            return 1;
        }

        /// <summary>
        /// �޸İ汾��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public int UpdateVersion()
        {
            return 1;
        }

        /// <summary>
        /// ɾ���汾��Ϣ
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