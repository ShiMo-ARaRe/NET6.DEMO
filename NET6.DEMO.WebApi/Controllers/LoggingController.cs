using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.Interfaces;
using NET6.DEMO.WebApi.Utility.Swagger;

//ע�⣺��������û���κμ�ص�ϵͳ���ߣ�
//��μ��-- - ��־��¼
//��Ҫ��־��Ϣ�ĳ־û�--���浽�ļ��У����浽���ݿ��У�

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// ��־��¼
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��ȡ��־��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public IActionResult GetLog()
        {
            // LogError �� LogInformation �� .NET Core �����õķ��������ڼ�¼��־��
            // ��Щ������ͨ�� ASP.NET Core �е�������־��¼����ILogger �ӿڵ�ʵ�֣��ṩ�ġ�
            _logger.LogError("LogError:=========Get Api������==========="); //���󼶱����־
            _logger.LogInformation("LogInformation:=========Get Api������==========="); //��Ϣ�������־
            #region ���ú������̨��ӡ
            // fail: NET6.DEMO.WebApi.Controllers.LoggingController[0]
            // LogError:========= Get Api������ ===========
            // info: NET6.DEMO.WebApi.Controllers.LoggingController[0]
            // LogInformation:========= Get Api������ ===========
            #endregion

            /* �ⲿ��ʽ
        log4net ��־��¼
        ֧���ı���־�����ݿ���־
        1�� Nuget �������� log4net + Microsoft.Extensions.Logging.Log4Net.AspNetCore
        2��׼�������ļ�������Ϊʼ�ո��ơ� // CfgFile�ļ�����log4net.Config�ļ����Ҽ����ԣ�
            ����������ó�ʼ�ո��ƣ���ô���ļ����ܾͲ��ᱻ���룩
        3��ֲ�� log4net
            builder.Logging.AddLog4Net("CfgFile/log4net.Config");
        4��ע��log����д��־��д�� txt �ļ� */

            return new JsonResult(new ApiResult<string>()
            {
                Success = true,
                Data = "��־��¼"
            });
        }

    }
}