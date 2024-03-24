using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NET6.DEMO.Interfaces;
using NET6.DEMO.Models;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/v{version:apiVersion}")]
    public class ReturnResultController : ControllerBase
    {
        private readonly ILogger<ReturnResultController> _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger"></param>
        public ReturnResultController(ILogger<ReturnResultController> logger)
        {
            _logger = logger;
        }

/*  ����ָ������ ��Specific type��
����ָ�����ͣ�����Ƕ���int��Ĭ�ϻ᷵�� Json ��ʽ---�������л�����ģ�
������ַ�����ֱ�ӷ����ַ�����
��򵥵� API �᷵��ԭ���Ļ��߸��ӵ��������ͣ����磬string �����Զ���������ͣ����������µ�
Action�������䷵����һ���Զ����User����ļ���    */
        #region ����Specific-type

        /// <summary>
        /// ����ApiResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentApiResult")]
        public ApiResult<Student> GetStudentApiResult(int id) => new ApiResult<Student>()
        {
            Success = true,
            Message = "��ѯ�ɹ�",
            Data = new Student()
            {
                Id = id,
                Name = "Richard",
                Age = 36
            }
        };

        /// <summary>
        /// Specific-type==����User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentById/{id:int}")]
        public Student GetStudentById(int id) => new Student()
        {
            Id = id,
            Age = 36,
            Name = "����"
        };

        /// <summary>
        /// Specific-type==���ؼ���
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentList")]
        public IEnumerable<Student> GetStudentList() => new List<Student>()
        /*�÷�������һ���ܹ����� Student ����ļ��ϡ�
        ʵ�����������ֱ�ӷ��� List<Student> ���ͣ������� IEnumerable<Student>��
        ��Ϊ List<T> ����ʵ���� IEnumerable<T> �ӿڣ������ǿɵ����ġ�
        ԭʼ������ʹ�� IEnumerable<Student> �ķ���������Ϊ�˸�ͨ�õر�ʾ���ؽ����
        ��Ϊ IEnumerable<T> ��һ��������Ľӿڣ�������Ӧ��ͬ���͵ļ��ϡ�*/
        {
            new Student()
            {
                Id = 1,
                Age = 36,
                Name = "����"
            }, new Student()
            {
                Id = 1,
                Age = 36,
                Name = "����"
            }
        };


        /// <summary>
        /// Specific-type==�첽����   ��asp.net core 3.1֮��
        /// </summary>
        /// <param name="studentService"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAsyncUserList")]
        public async IAsyncEnumerable<Student> GetAsyncUserList([FromServices] IStudentService studentService)
        //IAsyncEnumerable<T> �� C# 8.0 ������첽ö�����ӿڣ����������첽�ķ�ʽ�������ϵ�Ԫ�ء�

        {
            int t = 0; // ������
            // GetUserListAsync()�����ķ��ؽ������һ����ȫ�����أ���������ķ��أ��ȷ��ص�Ԫ�����������һ������
            var studentlist = studentService.GetUserListAsync(); //�����첽ö�����������ע��ֻ�ǽ��͡�
            Console.WriteLine(studentService.Equals("��������"));
            // ʹ��GetAuthorsAsync�첽����������Ҫ�ȴ�GetAuthorsAsync������ִ����ɣ��ͻ������һ��������
            /* ��ϸִ�й���
ִ�� await foreach ѭ��ǰ�������Ѿ������� GetUserListAsync() ��������ȡ�䷵�ص��첽ö������
�ڵ�һ�ε�����ʼǰ��await ����ͣѭ����ִ�У��ȴ��첽ö�������ɵ�һ��Ԫ�ء�
���첽ö��������һ����Ԫ��ʱ��await �ͻᱻ���
�����ɵ�Ԫ�ظ�ֵ��ѭ��������������������� student����Ȼ��ִ��ѭ�����ڵĴ��롣
��ѭ������ִ����Ϻ󣬿������̻�ص� await foreach ��䣬������һ�ε�����
await �ٴ���ͣѭ����ִ�У��ȴ��첽ö����������һ��Ԫ�ء�
�ظ����ϲ��裬ֱ���첽ö������������Ԫ�أ�ѭ��������
             */
            await foreach (var student in studentlist)
            /*await��yield
await �� yield ���ʹ�õ�Ŀ����ʵ���첽���ӳ�ö�٣�asynchronous lazy enumeration����

���첽����У���ʱ����Ҫ������������ݻ����漰����ʱ�Ĳ�����
ʹ�ô�ͳ��ͬ����ʽ����Ҫһ���Ի�ȡ���е����ݻ��ߵȴ�����������ɣ�����ܻᵼ������������߳�ʱ��ĵȴ���

���첽���ӳ�ö����������Ҫʱ������ɺͷ���Ԫ�أ�������Ҫ�ȴ�����������ɡ�
���������ڴ���������ݼ����ߺ�ʱ����ʱ�������ڴ����ĺ͵ȴ�ʱ�䣬��߳������Ӧ���ܡ�

������
�����������տ�һ��ֱ����Ŀ���������Ŀ�ǰ���һ����ʱ������ġ�
���ӳ�ö�ٷ�ʽ�£��㲻��Ҫ�ȴ�������Ŀ������ϲ��ܿ�ʼ�ۿ���
�����ڽ�Ŀ��ʼ������������տ�����Ŀ�����ݡ�
���ַ�ʽʹ����Լ�ʱ������ڲ��ŵ����ݣ�������Ҫ�ȴ����н�Ŀ������ϡ�
�ܽ���ǣ�����Խ��ӳ�ö�����Ϊʵʱ�ۿ�ֱ��������ʹ���ӳ�ö���������ڹۿ�¼����*/
            {
                //Console.WriteLine(t);
                //t++;
                yield return student;
                Console.WriteLine(t);   // ȫ����ӡ��˵��yield return����Ĵ����ִ�� 
                // ע�⣬���ڿ���̨�й۲��ӡ��Ϣ����ӡ������5�룬˵�������ӳ�ö��
                t++;
            }
            //Console.WriteLine(t); // ֻ��ӡһ��
            //t++;
        }

        /// <summary>
        /// ����һ��int����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInt")]
        public int GetInt()
        {
            return 12345;
        }

        /// <summary>
        /// ����һ��String����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetString")]
        public String GetString()
        {
            return "this is String";
        }

        #endregion


        //ֻҪ��ʵ���� IActionResult �Ľӿڵģ���������Ϊ����ֵ��
        #region ����IActionResult����
        /// <summary>
        /// ����IActionResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentApiResultIActionResult")]
        public IActionResult GetStudentApiResultIActionResult(int id) => new JsonResult(new ApiResult<Student>()
        {   // ��Json��ʽ����
            Success = true,
            Message = "��ѯ�ɹ�",
            Data = new Student()
            {
                Id = id,
                Name = "Richard",
                Age = 36
            }
        });


        /// <summary>
        /// ����IActionResult==GetStudentJson
        /// </summary>
        /// <returns></returns>
        [Route("GetStudentJson")]
        [HttpGet]
        public IActionResult GetStudentJson()
        {
            //����Json����
            //return new JsonResult(new Student()
            //{
            //    Id = 1,
            //    Age = 36,
            //    Name = "����"
            //});

            ////��ӦNofound
            return NotFound("No records");
            //���ڷ��� HTTP 404 Not Found ��Ӧ��һ�ַ�ʽ�������������ַ������ͣ�
            //���Ƿ���һ�� NotFoundObjectResult ���󣬸ö������һ������������Ϣ���ַ�����

            ////��ӦOk
            //return Ok(); // ������Ok�ȷ�����װ
        }

        #endregion


        #region ����ActionResult<T>

        /// <summary>
        /// ����ActionResult ����==���� 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentNew")]
        public ActionResult<Student> GetStudentNew()
        {
            //����Json����
            return new Student()
            {
                Id = 1,
                Age = 36,
                Name = "����"
            };
        }


        /// <summary>
        /// ���ط��͵�ActionResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentApiResultActionResultGeneric")]
        public ActionResult<ApiResult<Student>> GetStudentApiResultActionResultGeneric(int id) => new ApiResult<Student>()
        // ���ص�Ĭ�϶���JSON��ʽ
        {
            Success = true,
            Message = "��ѯ�ɹ�",
            Data = new Student()
            {
                Id = id,
                Name = "Richard",
                Age = 36
            }
        };


        #endregion

      
        /* �̶���ʽ
WebApi ��Ϊ������ڣ���Ҫ�͵������Խӣ�����ܹ�Լ��һ����׼���̶��ķ������ݵĸ�ʽ��
�̶���ʽ����
    public class ApiResult<T> where T : class
    {
        /// <summary>
        /// Apiִ���Ƿ�ɹ�
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        public T? Data { get; set; }
    }
         */

    }
}