using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 路由约束案例
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RouteConstraintController : ControllerBase
    {
        private readonly ILogger<RouteConstraintController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public RouteConstraintController(ILogger<RouteConstraintController> logger)
        {
            _logger = logger;
        }
          
        /// <summary>
        /// int 类型约束
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()] 
        [Route("IntConstraint/{id:int}")]
        public int IntConstraint(int id)
        {
            return id;
        }

        /// <summary>
        /// 布尔类型约束
        /// </summary>
        /// <param name="isOk"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("BoolConstraint/{isOk:bool}")]
        public bool BoolConstraint(bool isOk)
        {
            return isOk;
        }

        /// <summary>
        /// 时间类型约束
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("DateTimeConstraint/{datetime:dateTime}")]
        public DateTime DateTimeConstraint(DateTime datetime)
        {
            return datetime;
        }

        /// <summary>
        /// decimal类型约束
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("DecimalConstraint/{dec:decimal}")]
        public decimal DecimalConstraint(decimal dec)
        {
            return dec;
        }

        /// <summary>
        /// double类型约束
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("DoubleConstraint/{dec:double}")]
        public double DoubleConstraint(double dec)
        {
            return dec;
        }

        /// <summary>
        /// float类型约束
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("FloatConstraint/{dec:float}")]
        public float FloatConstraint(float dec)
        {
            return dec;
        }

        /// <summary>
        /// Guid类型约束
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("GuidConstraint/{guid:guid}")]
        public Guid GuidConstraint(Guid guid)
        {
            return guid;
        }

        /// <summary>
        /// long类型约束
        /// </summary>
        /// <param name="lon"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("LongConstraint/{lon:long}")]
        public long LongConstraint(long lon)
        {
            return lon;
        }
         
    }
}