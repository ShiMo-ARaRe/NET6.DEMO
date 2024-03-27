using Microsoft.AspNetCore.Authorization;
using NET6.DEMO.Interfaces;

namespace NET6.Demo.WebApi.Utility
{

    /// <summary>
    /// 
    /// </summary>
    public class NikeNameAuthorizationHandler : AuthorizationHandler<CustomNickNameRequirement>
    {

        private IStudentService _IStudentService;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="studentService"></param>
        public NikeNameAuthorizationHandler(IStudentService  studentService)
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
        {
            if (context.User.Claims.Count() == 0)
            {
                return Task.CompletedTask; //验证失败的
            }

            string nickName = context.User.Claims.First(c => c.Type == "NickName").Value;

            if (_IStudentService.Validata(nickName))
            {
                context.Succeed(requirement); //验证成功后
            } 
            return Task.CompletedTask;
        }
    }
}
