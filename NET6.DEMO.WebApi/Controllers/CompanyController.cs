using Microsoft.AspNetCore.Mvc;
//using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// ��˾��Դ��Դ---��һ�汾
    /// </summary>
    [ApiController]  
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger"></param>
        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        //��Щע�͵�Ŀ�����ṩ�����󷽷��ļ�Ҫ˵�����ĵ�����ͨ������Ϊ�������ĵ�ע�ͻ򷽷���ժҪע�͡�
        /// <summary>
        /// ��ȡ��˾��Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Company")]
        public Company GetCompany()
        {
            //new()û��ָ�����ͣ�����Ϊ��C# 9.0��������һ�ֳ�Ϊ"target-typed new ���ʽ"�����﷨��
            //�����﷨������û����ʽָ�����͵�����£����������Ľ��������ƶϡ�
            return new()
            {
                Id = 123,
                Name = "Richard",
                AddRess = "�����人-������---��һ�汾"
            };
        }

        /// <summary>
        /// ������˾��Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost(Name = "Company")]
        public int AddCompany(Company user)
        {
            return 1;
        }

        /// <summary>
        /// �޸Ĺ�˾��Ϣ
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPut(Name = "Company")]
        public int UpdateCompany(Company company)
        {
            return 1;
        }


        /// <summary>
        /// ɾ����˾��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(Name = "Company")]
        public int DeleteCompany(int id)
        {
            return 1;
        }

        // ʹ��Name����Ϊÿ�����󷽷�ָ����ͬ�����ƣ�"Company"����Ϊ��Ϊÿ����������һ��·�����ơ�
        // ��ĳЩ����£������ж���������ͣ���GET��POST��PUT��DELETE�ȣ���Ҫʹ����ͬ��·���߼���
        // ͨ��Ϊ����ָ����ͬ�����ƣ�������·��������������ͬ�Ĵ����߼�����ߴ���Ŀ�ά���ԺͿ������ԡ�
        // ����Ҫע����ǣ���4����������ֶ���Company,��ᵼ��Swaggerʹ�ò��㡣
    }
}