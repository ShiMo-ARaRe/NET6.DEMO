using Microsoft.AspNetCore.Mvc;
//using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// 公司资源资源---第一版本
    /// </summary>
    [ApiController]  
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        //这些注释的目的是提供对请求方法的简要说明和文档。它通常被称为方法的文档注释或方法的摘要注释。
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Company")]
        public Company GetCompany()
        {
            //new()没有指定类型，是因为在C# 9.0中引入了一种称为"target-typed new 表达式"的新语法。
            //这种语法允许在没有显式指定类型的情况下，根据上下文进行类型推断。
            return new()
            {
                Id = 123,
                Name = "Richard",
                AddRess = "湖北武汉-来自于---第一版本"
            };
        }

        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost(Name = "Company")]
        public int AddCompany(Company user)
        {
            return 1;
        }

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPut(Name = "Company")]
        public int UpdateCompany(Company company)
        {
            return 1;
        }


        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(Name = "Company")]
        public int DeleteCompany(int id)
        {
            return 1;
        }

        // 使用Name属性为每个请求方法指定相同的名称（"Company"）是为了为每个方法创建一个路由名称。
        // 在某些情况下，可能有多个请求类型（如GET、POST、PUT、DELETE等）需要使用相同的路由逻辑。
        // 通过为它们指定相同的名称，可以在路由配置中重用相同的代码逻辑，提高代码的可维护性和可重用性。
        // 不过要注意的是，这4个请求的名字都叫Company,这会导致Swagger使用不便。
    }
}