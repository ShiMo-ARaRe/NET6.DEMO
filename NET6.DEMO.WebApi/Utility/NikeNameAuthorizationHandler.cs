//引入Microsoft.AspNetCore.Authorization命名空间，其中包含了授权相关的类和接口。
using Microsoft.AspNetCore.Authorization;
using NET6.DEMO.Interfaces; // 引入接口层

namespace NET6.Demo.WebApi.Utility
{

    /// <summary>
    /// 
    /// </summary>
    public class NikeNameAuthorizationHandler : AuthorizationHandler<CustomNickNameRequirement>
    /* AuthorizationHandler<CustomNickNameRequirement> 是一个用于处理 特定授权需求 的基类。
       在 ASP.NET Core 的授权机制中，我们可以定义自己的授权需求，这些需求可以基于业务逻辑或其他自定义条件。
       为了处理这些自定义的授权需求，我们需要创建一个继承自 AuthorizationHandler<TRequirement> 的类，
       其中 TRequirement 是 自定义授权需求 的类型（比如我们这里是CustomNickNameRequirement）。
       该类的HandleRequirementAsync 方法是一个 重写的授权处理程序 方法，用于处理特定授权需求的逻辑。 */
    {

        private IStudentService _IStudentService;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="studentService"></param>
        public NikeNameAuthorizationHandler(IStudentService  studentService) //依赖注入
        {
            this._IStudentService = studentService;
        }

        /// <summary>
        /// 可以在这里写入验证逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomNickNameRequirement requirement)
        /*  HandleRequirementAsync 是 AuthorizationHandler<TRequirement> 类中的一个方法，用于处理特定授权需求的逻辑。
            context 参数是一个 AuthorizationHandlerContext 对象，它包含了与授权相关的上下文信息，例如当前用户、策略要求等。
            requirement 参数是一个 CustomNickNameRequirement 对象，表示特定的授权需求。   */
        {
            Console.WriteLine($"###### 检查用户的声明数量 {context.User.Claims.Count()}");
            context.User.Claims.Any(c =>
            {
                Console.WriteLine($"###### 声明 {c.Type}");
                return false;
            });

            if (context.User.Claims.Count() == 0)
            {
                /*方法中的代码 if (context.User.Claims.Count() == 0) 检查用户的声明数量。
                如果用户没有任何声明，即 Claims 集合为空，那么该方法直接返回 Task.CompletedTask，表示验证失败。*/
                return Task.CompletedTask; //验证失败的
            }

            //从用户的声明中查找类型为 "NickName" 的声明，并获取其值。
            string nickName = context.User.Claims.First(c => c.Type == "NickName").Value;

            if (_IStudentService.Validata(nickName)) //用于验证昵称是否有效，Validata是我们自己写的逻辑
            {
                //表示授权成功，Succeed代表成功
                context.Succeed(requirement); //验证成功后
            } 
            return Task.CompletedTask; //表示任务已完成,是否成功取决于前面
        }
    }
}
