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
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public ReturnResultController(ILogger<ReturnResultController> logger)
        {
            _logger = logger;
        }

/*  返回指定类型 （Specific type）
返回指定类型，如果是对象、int，默认会返回 Json 格式---经过序列化处理的；
如果是字符串：直接返回字符串；
最简单的 API 会返回原生的或者复杂的数据类型（比如，string 或者自定义对象类型）。考虑如下的
Action方法，其返回了一个自定义的User对象的集合    */
        #region 返回Specific-type

        /// <summary>
        /// 返回ApiResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentApiResult")]
        public ApiResult<Student> GetStudentApiResult(int id) => new ApiResult<Student>()
        {
            Success = true,
            Message = "查询成功",
            Data = new Student()
            {
                Id = id,
                Name = "Richard",
                Age = 36
            }
        };

        /// <summary>
        /// Specific-type==返回User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentById/{id:int}")]
        public Student GetStudentById(int id) => new Student()
        {
            Id = id,
            Age = 36,
            Name = "张三"
        };

        /// <summary>
        /// Specific-type==返回集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentList")]
        public IEnumerable<Student> GetStudentList() => new List<Student>()
        /*该方法返回一个能够迭代 Student 对象的集合。
        实际上这里可以直接返回 List<Student> 类型，而不是 IEnumerable<Student>，
        因为 List<T> 类型实现了 IEnumerable<T> 接口，并且是可迭代的。
        原始代码中使用 IEnumerable<Student> 的返回类型是为了更通用地表示返回结果，
        因为 IEnumerable<T> 是一个更抽象的接口，可以适应不同类型的集合。*/
        {
            new Student()
            {
                Id = 1,
                Age = 36,
                Name = "张三"
            }, new Student()
            {
                Id = 1,
                Age = 36,
                Name = "李四"
            }
        };


        /// <summary>
        /// Specific-type==异步返回   在asp.net core 3.1之后
        /// </summary>
        /// <param name="studentService"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAsyncUserList")]
        public async IAsyncEnumerable<Student> GetAsyncUserList([FromServices] IStudentService studentService)
        //IAsyncEnumerable<T> 是 C# 8.0 引入的异步枚举器接口，它允许以异步的方式迭代集合的元素。

        {
            int t = 0; // 测试用
            // GetUserListAsync()方法的返回结果不会一次性全部返回，而是逐个的返回，先返回的元素立马进行下一步处理
            var studentlist = studentService.GetUserListAsync(); //返回异步枚举器，上面的注释只是解释。
            Console.WriteLine(studentService.Equals("哈哈哈哈"));
            // 使用GetAuthorsAsync异步方法，不需要等待GetAuthorsAsync方法的执行完成，就会进入下一步迭代。
            /* 详细执行过程
执行 await foreach 循环前，我们已经调用了 GetUserListAsync() 方法并获取其返回的异步枚举器。
在第一次迭代开始前，await 会暂停循环的执行，等待异步枚举器生成第一个元素。
当异步枚举器生成一个新元素时，await 就会被激活，
将生成的元素赋值给循环变量（在这种情况下是 student），然后执行循环体内的代码。
在循环体内执行完毕后，控制流程会回到 await foreach 语句，继续下一次迭代。
await 再次暂停循环的执行，等待异步枚举器生成下一个元素。
重复以上步骤，直到异步枚举器不再生成元素，循环结束。
             */
            await foreach (var student in studentlist)
            /*await与yield
await 和 yield 配合使用的目的是实现异步的延迟枚举（asynchronous lazy enumeration）。

在异步编程中，有时候需要处理大量的数据或者涉及到耗时的操作。
使用传统的同步方式，需要一次性获取所有的数据或者等待整个操作完成，这可能会导致性能问题或者长时间的等待。

而异步的延迟枚举允许在需要时逐个生成和返回元素，而不需要等待整个操作完成。
这样可以在处理大型数据集或者耗时操作时，减少内存消耗和等待时间，提高程序的响应性能。

举例：
假设你正在收看一档直播节目，而这个节目是按照一定的时间表播出的。
在延迟枚举方式下，你不需要等待整个节目播放完毕才能开始观看。
而是在节目开始后，你可以立即收看到节目的内容。
这种方式使你可以即时获得正在播放的内容，而不需要等待所有节目播放完毕。
总结就是：你可以将延迟枚举类比为实时观看直播，而不使用延迟枚举则类似于观看录播。*/
            {
                //Console.WriteLine(t);
                //t++;
                yield return student;
                Console.WriteLine(t);   // 全部打印，说明yield return后面的代码会执行 
                // 注意，请在控制台中观察打印信息，打印将持续5秒，说明这是延迟枚举
                t++;
            }
            //Console.WriteLine(t); // 只打印一次
            //t++;
        }

        /// <summary>
        /// 返回一个int类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInt")]
        public int GetInt()
        {
            return 12345;
        }

        /// <summary>
        /// 返回一个String类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetString")]
        public String GetString()
        {
            return "this is String";
        }

        #endregion


        //只要是实现了 IActionResult 的接口的，都可以作为返回值；
        #region 返回IActionResult类型
        /// <summary>
        /// 返回IActionResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentApiResultIActionResult")]
        public IActionResult GetStudentApiResultIActionResult(int id) => new JsonResult(new ApiResult<Student>()
        {   // 以Json格式返回
            Success = true,
            Message = "查询成功",
            Data = new Student()
            {
                Id = id,
                Name = "Richard",
                Age = 36
            }
        });


        /// <summary>
        /// 返回IActionResult==GetStudentJson
        /// </summary>
        /// <returns></returns>
        [Route("GetStudentJson")]
        [HttpGet]
        public IActionResult GetStudentJson()
        {
            //返回Json对象
            //return new JsonResult(new Student()
            //{
            //    Id = 1,
            //    Age = 36,
            //    Name = "张三"
            //});

            ////响应Nofound
            return NotFound("No records");
            //用于返回 HTTP 404 Not Found 响应的一种方式。它并不返回字符串类型，
            //而是返回一个 NotFoundObjectResult 对象，该对象包含一个包含错误消息的字符串。

            ////响应Ok
            //return Ok(); // 必须有Ok等方法包装
        }

        #endregion


        #region 返回ActionResult<T>

        /// <summary>
        /// 返回ActionResult 泛型==返回 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentNew")]
        public ActionResult<Student> GetStudentNew()
        {
            //返回Json对象
            return new Student()
            {
                Id = 1,
                Age = 36,
                Name = "张三"
            };
        }


        /// <summary>
        /// 返回泛型的ActionResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetStudentApiResultActionResultGeneric")]
        public ActionResult<ApiResult<Student>> GetStudentApiResultActionResultGeneric(int id) => new ApiResult<Student>()
        // 返回的默认都是JSON格式
        {
            Success = true,
            Message = "查询成功",
            Data = new Student()
            {
                Id = id,
                Name = "Richard",
                Age = 36
            }
        };


        #endregion

      
        /* 固定格式
WebApi 作为服务存在，需要和第三方对接；最好能够约束一个标准；固定的返回数据的格式；
固定格式如下
    public class ApiResult<T> where T : class
    {
        /// <summary>
        /// Api执行是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 结果集
        /// </summary>
        public T? Data { get; set; }
    }
         */

    }
}