//引入Microsoft.AspNetCore.Authorization命名空间，其中包含了授权相关的类和接口。
using Microsoft.AspNetCore.Authorization;
using NET6.Demo.WebApi.Utility;

//CustomNickNameRequirement 是一个自定义的授权需求类，实现了 IAuthorizationRequirement 接口。
//它可以定义特定的授权需求，例如要求用户具有特定的昵称。

namespace NET6.Demo.WebApi.Utility
{

    /// <summary>
    /// 
    /// </summary>
    public class CustomNickNameRequirement: IAuthorizationRequirement
    {
    }
}
