//������ ASP.NET Core MVC �������ռ䣬���а��������ڴ��� Web Ӧ�ó���Ŀ������������������������������ԡ�
using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.Interfaces; // ����ӿ�

namespace NET6.DEMO.WebApi.Controllers
{

    /// <summary>
    /// ������Դ�����������������Եĺ��ļ�ֵ
    /// 1  FromServices 
    /// 2  FromBody 
    /// 3  FromForm 
    /// 4  FromHeader 
    /// 5  FromQuery 
    /// 6  FromRoute
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class ParameterFromController : ControllerBase
    {
        private readonly ILogger<ParameterFromController> _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger"></param>
        public ParameterFromController(ILogger<ParameterFromController> logger)
        {
            _logger = logger;
        }

        //Ϊ��������ܣ���ô˵��
        //�����ITestServiceBд�����캯�����ô�����������ÿ��API�������ʱ����ȥ��ʼ��ITestServiceB��
        //����ITestServiceB�ò��ϣ�Ҳ���ʼ�����˷�����!

        #region FromServices 
        /// <summary>
        /// FromServices����
        /// 1.��ʾ������IOC��������
        /// 2.��Ȼ��ҪIOC������ע��
        /// 
        /// ���û�б�� [FromServices]��Ĭ�ϻ��϶����������Ҫͨ�����÷�����
        /// 
        /// �����������Ѿ����˹��캯��ע����Ϊʲô�����л��ṩ[FromServices]�����ڷ����Ĳ���������ȡ����ʵ���أ� Ϊ���������~
        /// 
        /// </summary>
        /// <param name="ITestServiceB"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromServicesMethod")]
        public string FromServicesMethod([FromServices]ITestServiceB ITestServiceB)
        {
            // ������̨��ӡ���ȹ���ITestServiceA���ٹ���ITestServiceB
            return ITestServiceB.ShowB();
        }

        #endregion

        //��������F12��Networkʳ�ø���

        //api �Ѽ������ڿͻ�������Ĳ����У���HTTP Body��ȥ�Ѽ�������������ݣ�ͨ������ȡ JSON,XML,�ռ����Ժ�,�󶨵���ǰ�Ĳ���/�����У�
        #region FromBody���� 

        /// <summary>
        /// FromBody����-Get����---���ܷ���
        /// </summary>
        /// <param name="user"></param> //param�ǲ�������˼
        /// <returns></returns>
        [HttpGet()]
        [Route("FromBodyMethodGet")]
        public User FromBodyMethodGet([FromBody] User user)
        {
            return user;
        }

        /// <summary>
        /// FromBody����-Post����---���Է���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromBodyMethodPost")]
        public User FromBodyMethodPost([FromBody] User user)
        {
            return user;
        }

        /// <summary>
        /// FromBody����-Put����---���Է���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromBodyMethodPut")]
        public User FromBodyMethodPut([FromBody] User user)
        {
            return user;
        }

        /// <summary>
        /// FromBody����-Delete����---���Է���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromBodyMethodDelete")]
        public User FromBodyMethodDelete([FromBody] User user)
        {
            return user;
        }

        #endregion

        //api �Ѽ������ڿͻ�������Ĳ����У���Form����ȥ�Ѽ�������������ݣ��ռ����Ժ󣬰󶨵���ǰ�Ĳ���/�����У�
        #region FromForm����

        /// <summary>
        /// FromForm����-Get����---���ܷ���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromFormMethodGet")]
        public User FromFormMethodGet([FromForm] User user)
        {
            return user;
        }

        /// <summary>
        /// FromForm����-Post����---���Է���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromFormMethodPost")]
        public User FromFormMethodPost([FromForm] User user)
        {
            return user;
        }

        /// <summary>
        /// FromForm����-Put����---���Է���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromFormMethodPut")]
        public User FromFormMethodPut([FromForm] User user)
        {
            return user;
        }

        /// <summary>
        /// FromForm����-Delete����---���Է���
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromFormMethodDelete")]
        public User FromFormMethodDelete([FromForm] User user)
        {
            return user;
        }

        #endregion

        //api �Ѽ������ڿͻ�������Ĳ����У���Headerͷ��Ϣ��ȥ�Ѽ�������������ݣ��ռ����Ժ󣬰󶨵���ǰ�Ĳ���/�����У�
        #region FromHeader


        /// <summary>
        /// FromHeader����-Get����---���Է���
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromHeaderMethodGet")]
        public string FromHeaderMethodGet([FromHeader] string header)
        {
            return header;
        }

        /// <summary>
        /// FromHeader����-Post����---���Է���
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromHeaderMethodPost")]
        public string FromHeaderMethodPost([FromHeader] string header)
        {
            return header;
        }

        /// <summary>
        /// FromHeader����-Put����---���Է���
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromHeaderMethodPut")]
        public string FromHeaderMethodPut([FromHeader] string header)
        {
            return header;
        }

        /// <summary>
        /// FromHeader����-Delete����---���Է���
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromHeaderMethodDelete")]
        public string FromHeaderMethodDelete([FromHeader] string header)
        {
            return header;
        }

        #endregion

        //����ͻ���ͨ����ѯ�ַ�����ʽ���ݲ����� FromQuery ������ Url ��ַ��ȥ��ȡֵapi �Ѽ������ڿͻ�������Ĳ����У�
        //ͨ��URL Query��ȥ�Ѽ�������������ݣ��ռ����Ժ󣬰󶨵���ǰ�Ĳ���/�����У�
        #region FromQuery����

        /// <summary>
        /// FromQuery����-Get����
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromQueryMethodGet")]
        public string FromQueryMethodGet([FromQuery] string query) // ����������Ҳ����Ҫ��
        {
            return query;
        }

        /// <summary>
        /// FromQuery����-Post����
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromQueryMethodPost")]
        public string FromQueryMethodPost([FromQuery] string query)
        {
            return query;
        }

        /// <summary>
        /// FromQuery����-Put����
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromQueryMethodPut")]
        public string FromQueryMethodPut([FromQuery] string query)
        {
            return query;
        }

        /// <summary>
        /// FromQuery����-Delete����
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromQueryMethodDelete")]
        public string FromQueryMethodDelete([FromQuery] string query)
        {
            return query;
        }

        #endregion

        //api �Ѽ������ڿͻ�������Ĳ����У���·����ȥ�Ѽ�������������ݣ��ռ����Ժ󣬰󶨵���ǰ�Ĳ���/�����У�
        #region FromRoute����
         
        /// <summary>
        ///  FromRoute����--Get����--
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FromRouteMethodGet/{route}")]
        public string FromRouteMethodGet([FromRoute] string route,string text) // ��ʱ�����ռ�����������һ��ͨ��·�ɣ�һ��ͨ����ѯ����
        {
            return route;
        }

        /// <summary>
        /// FromRoute����--Post����
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("FromRouteMethodPost/{route}")]
        public string FromRouteMethodPost([FromRoute] string route)
        {
            return route;
        }

        /// <summary>
        /// FromRoute����--Put����
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("FromRouteMethodPut/{route}")]
        public string FromRouteMethodPut([FromRoute] string route)
        {
            return route;
        }
         
        /// <summary>
        /// FromRoute����--Delete����
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("FromRouteMethodDelete/{route}")]
        public string FromRouteMethodDelete([FromRoute] string route)
        {
            return route;
        }
        #endregion 
    }
}