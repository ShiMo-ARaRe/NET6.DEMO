//��������ע��Ϳ��Ʒ�ת��Inversion of Control��IoC�������ĸ��


//������ ASP.NET Core MVC �������ռ䣬���а��������ڴ��� Web Ӧ�ó���Ŀ������������������������������ԡ�
using Microsoft.AspNetCore.Mvc;
//������ NET6.DEMO.Interfaces �����ռ䣬���а������Զ���Ľӿ����͵Ķ��塣ͨ������������ռ䣬�����ڴ�����ʹ����Щ�ӿ����͡�
using NET6.DEMO.Interfaces;
//������ NET6.DEMO.WebApi.Utility.Swagger �����ռ䣬���а������� Swagger ��صĹ���������ԡ�
//Swagger ��һ��������ơ��������ĵ��������� Web API �Ĺ��ߣ�
//�������ռ��ṩ���� ASP.NET Core Web API ��Ŀ��ʹ�� Swagger �Ĺ��ߺ͹��ܡ�
using NET6.DEMO.WebApi.Utility.Swagger;

//using NET6.DEMO.Services; // ���Ǵ�ͳ����������������IOC������ʵ��
/* ��ͳ������

�ڴ�ͳ�������У�Ϊ�˻�ȡ����ķ���ʵ����ͨ����ֱ��ʹ�� using ����������������ռ䣬
���� NET6.DEMO.Services��Ȼ��ͨ��ʵ�����������ռ��µ�������ȡ�����ʵ����
������������һЩ���⣬������֮���������ϵ��ǿ�����Խ��н���Ͳ��ԡ�
ͬʱ�������Ҫ�ڲ�ͬ�ĵط��������������ķ���ʵ�֣���Ҫ�޸Ĵ����Ĵ��롣

ʹ�� IoC ������

���ڵ�������ʹ�ÿ��Ʒ�ת��IoC������������ͽ������֮���������ϵ��
IoC ������һ��ʵ������ע��Ļ��ƣ������𴴽��͹��������������ڣ�
��������ķ���ʵ��ע�뵽��Ҫ�ĵط����Ӷ�ʵ�ֶ���֮��Ľ��������ԡ�
ͨ��ʹ�� IoC ������������Ҫ�ڴ�����ֱ��ʵ��������ķ����࣬���ǽ�������ϵ�����ý�������������
������������ú�Լ�����Զ������ʹ�������ķ���ʵ����������ע�뵽��Ҫ�ĵط���
ʹ�� IoC ����������ߴ���Ŀ�ά���ԡ��ɲ����ԺͿ���չ�ԣ����ҿ��Ը�������л����滻����ķ���ʵ�֡�
��ˣ����ע���е����ݱ����һ�����ִ��������Ƴ����������ʹ�� IoC ����������������ϵ��
������ֱ���������������ռ䲢ʵ���������ࡣ��������ʵ�ָ���ɢ����ϡ��������滻�͸��õĴ����ά���ԡ�
 */


namespace NET6.DEMO.WebApi.Controllers // ���������ռ�
{
    [ApiController] // �������һ��API������
    [ApiVersion("1.0")] // ָ��API�汾Ϊ1.0
    [Route("[controller]/v{version:apiVersion}")] // ����·��ģ��
    public class IOCContainerController : ControllerBase // ����һ����ΪIOCContainerController���࣬�̳���ControllerBase
    {
        private readonly ILogger<IOCContainerController> _logger; // ����һ��ILogger<IOCContainerController>���͵�˽��ֻ���ֶ�_logger
        private readonly ITestServiceA _ITestServiceA; // ����һ��ITestServiceA���͵�˽��ֻ���ֶ�_ITestServiceA
        private readonly ITestServiceB _ITestServiceB; // ����һ��ITestServiceB���͵�˽��ֻ���ֶ�_ITestServiceB

        //IServiceProvider �� ASP.NET Core �е�һ���ӿڣ���������һ�����ڻ�ȡ����ʵ���Ļ��ơ�
        //��������ע�루Dependency Injection���ĺ��Ľӿ�֮һ��
        private readonly IServiceProvider _IServiceProvider;


        // ���幹�캯�� // ��������캯���ϴ�ϵ㿴���������
        public IOCContainerController(ILogger<IOCContainerController> logger, ITestServiceA iTestServiceA, ITestServiceB iTestServiceB, IServiceProvider iServiceProvider)
        /* ����ÿ������ITestServiceAʱ��������һ���µ�TestServiceAʵ����
         ���ԣ���IOCContainerController�Ĺ��캯����Ҫһ��ITestServiceAʵ��ʱ��
         ����ע�������ͻ��Զ�����һ��TestServiceAʵ����������ע�뵽���캯���С�*/

        // ע�⣺������������֧�ֹ��캯��ע�롿�����������ShowA������Ҫ�õ�IServiceProvider

        {
            _logger = logger; 
            _ITestServiceA = iTestServiceA; 
            _ITestServiceB = iTestServiceB; 
            _IServiceProvider = iServiceProvider;
            // �ӿڱ����ܱ�ʵ��������������ע��ģʽ�У�����ͨ����ע��һ���ӿڵ���ʵ�����ӳ�䡣
            // ���������ӿڱ�ע�뵽��������ʱ������ע���������Զ�������ʵ�����ʵ����
        }

        [HttpGet()] // �������һ��HTTP GET����
        public string ShowA([FromServices] ITestServiceB iTestServiceBNew, [FromServices] IServiceProvider iServiceProvider)
        // �÷����Ĳ�����ʹ����[FromServices] ���ԣ�����ʾ������������ֵ��ͨ������ע����������ע�롣
        // Ҳ����˵��������ShowA����ʱ��ASP.NET Core���Զ�������ע�������л�ȡITestServiceB��IServiceProvider��ʵ����
        // �����ݸ�ShowA������

        {

            // GetService��ʾ��ȡ���񣬱���<>����д��ITestServiceB�ӿڣ����ܷ����µ�TestServiceBʵ��
            ITestServiceB testServiceB1 = _IServiceProvider.GetService<ITestServiceB>()!; 
            // ��_IServiceProvider�����Ǳ��������е��ֶΣ��л�ȡITestServiceB���͵ķ���ʵ������ֵ��testServiceB1
            
            // ���������������Ч����ȫһ����

            ITestServiceB testServiceB2 = iServiceProvider.GetService<ITestServiceB>()!;
            // ��iServiceProvider�����ǵ�ǰ�������»�ȡ�ģ��л�ȡITestServiceB���͵ķ���ʵ������ֵ��testServiceB2

            /* TestServiceB��������TestServiceA��
            ITestServiceA��ʵ����ͨ��TestServiceB�Ĺ��캯����������ġ���������ע���һ�ַ�ʽ����Ϊ���캯��ע�롣
            ������TestServiceB��ʵ��ʱ������ע���������Զ�����һ��ITestServiceA��ʵ������һ��TestServiceA����
           �������ʵ����ȡ����������ע�����������ע��ITestServiceA�ӿڵģ��������䴫��TestServiceB�Ĺ��캯����
            */

            /*����!��
            ����Ĵ���_IServiceProvider.GetService<ITestServiceB>()!�У�GetService<ITestServiceB>()��������
            �᷵��null�����û���ҵ���Ӧ�ķ��񣩡�����ͨ������������!������߱�������ȷ��������Ϊnull��
            ��˱���������Ϊ�˲������档
            */

            // ����������ʾ��ûʲô�����߼���
            //return _ITestServiceA.ShowA(); // ����_ITestServiceA��ShowA�����������ؽ�� // ��Ҳ�ǿ����������е��ֶ�
            return testServiceB2.ShowB(); // ����testServiceB2��ShowB�����������ؽ��

        }

        [HttpPost()] // �������һ��HTTP POST����
        public string ShowB() // ����һ����ΪShowB�ķ���������ֵ����Ϊstring
        {
            return _ITestServiceB.ShowB(); // ����_ITestServiceB��ShowB�����������ؽ��
        }
    }
}
