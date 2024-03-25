/*  Core WebAPI-Filter
 
 * ʲô�� AOP ?
 
    AOP��Aspect Orient Programming�� ������������, ��Ϊ��������̵�һ�ֲ���, �����ڲ��ƻ�
    ֮ǰ�ķ�װΪ������̬����һЩ���ܣ��Ӷ���ϵͳ���߱���չ�ԣ�
    ����һ�����湦��
    ������־�Ĺ���
    ��ϣ����ҪΥ������ԭ��Ҳϣ���ܹ������µĹ��ܣ�
    ��֮ǰ��ҵ���߼�֮ǰ�������߼���
    ��֮ǰ��ҵ���߼�֮���������߼���

 Core WebAPI �е� AOP ֧������Щ��

    ��Ȩ--- Authorize
    ��Դ-- Resource
    �쳣-- Exception
    ����ǰ��--- Action
    AlwayRunResult
    ���ǰ��--- Result
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET6.DEMO.Interfaces;
using NET6.DEMO.WebApi.Utility.Filters; // ���������
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// AOP-Filter
    /// </summary>
    [ApiController]
    //[CustomExceptionFilter] //��ǰ�������µ����еķ���������Ч(�����쳣����չ
    [ApiVersion("2.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class FiltersController : ControllerBase
    {
        private readonly ILogger<FiltersController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public FiltersController(ILogger<FiltersController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        //[CustomResourceFilter] //������չ
        //[CustomAsyncResourceFilter] //������չ(�첽�汾��
        //[CustomCacheResourceFilter] //��չ����
        [CustomAsyncCacheResourceFilter]//��չ�첽�汾����

        public IActionResult GetUser(int id) // ֻҪ��ʵ���� IActionResult �Ľӿڵģ���������Ϊ����ֵ��
        {

            Console.WriteLine("=============ҵ���߼�����=================");
            Console.WriteLine("=============ҵ���߼�����=================");
            Console.WriteLine("=============ҵ���߼�����=================");
            var data = new
            {
                Id = id,
                Name = "FFFF",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            ////IAsyncResourceFilter 
            ////IResourceFilter 
            return new JsonResult(data);    //����Json����
        }

        /// <summary>
        /// ����actionFilter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //[CustomActionFilter]
        //[CustomAsyncActionFilter]
        //[CustomCacheActionFilterAttribute]

        //����TypeFilter �� ServiceFilter�Ļ��Ϳ��Դ������ˣ�Ҳ����˵�����д�����ܴ�������

        //[TypeFilter(typeof(CustomLogActionFilterAttribute))]
        [ServiceFilter(typeof(CustomLogActionFilterAttribute))]
        //[CustomExceptionFilter]

        public IActionResult ActionFilterApi(int id)
        // ע�⣡�����id����query��������ʽ���ݵģ������ᵼ��Url�ĸı䣬���淽ʽ���ܲ��������Ԥ�ڣ�
        {
            //IActionFilter
            //IAsyncActionFilter 
            //ActionFilterAttribute
            _logger.LogInformation("FiltersController...ActionFilterApi ��ִ��~~~");
            Console.WriteLine("=============ҵ���߼�����=================");
            Console.WriteLine("=============ҵ���߼�����=================");
            Console.WriteLine("=============ҵ���߼�����=================");
            var data = new
            {
                Id = id,
                Name = "FFFF",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(data);
        }

        /// <summary>
        /// �쳣����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpPut]
        //[CustomExceptionFilter]

        /*����һ���Զ���������࣬������Ϊ���ṩ������ʹ�÷�ʽ�������ġ�
        ��ֱ�ӽ� CustomAsyncExceptionFilterAttribute ������Ӧ���ڿ���������������
        ����������������Ҫ��λ��ֱ��ʹ��[CustomAsyncExceptionFilter] ���ԣ���������ʽָ�����������͡�*/
        //[CustomAsyncExceptionFilter]

        //����TypeFilter �� ServiceFilter�Ļ��Ϳ��Դ������ˣ�Ҳ����˵�����д�����ܴ�������

        /*���� ASP.NET Core �ṩ�����ԣ�������ʽָ��ҪӦ�õĹ��������͡�
        ͨ��[TypeFilter] ���ԣ�������ֱ�ӽ� CustomAsyncExceptionFilterAttribute ������Ӧ�����ض��Ŀ���������������*/
        //[TypeFilter(typeof(CustomAsyncExceptionFilterAttribute))]

        /*��Ҳ�� ASP.NET Core �ṩ�����ԣ����ڽ� CustomAsyncExceptionFilterAttribute ��������Ϊ���������
         Ӧ���ڿ���������������ͨ�� [ServiceFilter] ���ԣ�������������Ӧ�ó�����ע�Ტ�Զ�Ӧ�øù�������*/
        [ServiceFilter(typeof(CustomAsyncExceptionFilterAttribute))] //��Ҫ��IOC������ע��CustomAsyncExceptionFilterAttribute

        public IActionResult ExceptionFilterApi(int id)
        { 
            //IExceptionFilter
            //IAsyncExceptionFilter 
            {
                int i = 0;
                int y = 23;
                int x = y / i;//���Գ���0  ������ģ���Ȼ�쳣
            }

            var data = new
            {
                Id = id,
                Name = "Richard��ʦ",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(new ApiResult<object>()
            {
                Success = true,
                Message = "Ok",
                Data = data
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult ResultFilterApi(int id)
        { 
            var data = new
            {
                Id = id,
                Name = "Richard��ʦ",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(new ApiResult<object>()
            {
                Success = true,
                Message = "Ok",
                Data = data
            });
        }

        // �����ResultFilter��AlwaysRunResultFilter �õıȽ���

        /// <summary>
        /// ����ResultFilter/AlwaysRunResultFilter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ResultResultFilterApi")]
        //[CustomResultFilter]
        //[CustomAsyncResultFilter]
        [CustomAlwayRunResultFilter]
        public IActionResult ResultResultFilterApi(int id)
        { 

            var data = new
            {
                Id = id,
                Name = "Richard��ʦ",
                Age = 36,
                DateTime = DateTime.Now.ToString()
            };
            return new JsonResult(new ApiResult<object>()
            {
                Success = true,
                Message = "Ok",
                Data = data
            });
        }
    }
}