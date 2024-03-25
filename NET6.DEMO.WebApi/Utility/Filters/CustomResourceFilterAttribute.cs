//这是 ASP.NET Core 中用于处理过滤器的命名空间。它包含了一些接口和类，用于定义和管理不同类型的过滤器。
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using static System.Net.WebRequestMethods;

namespace NET6.DEMO.WebApi.Utility.Filters
{
    /// <summary>
    /// 自定义ResouceFilter
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
        //Attribute 类是.NET 中的基类，用于定义自定义特性。
        //特性是一种元数据，可以应用于类、方法、属性等程序元素，以提供额外的信息或行为。
        //通过继承 Attribute 类，我们可以创建自定义特性，并将其应用到相关的程序元素上。

        //IResourceFilter 接口是 ASP.NET Core 中的一个接口，用于定义资源过滤器。
        //它继承自 IFilterMetadata 接口，表示该接口是一个过滤器。
        //IResourceFilter 接口定义了两个方法：OnResourceExecuting 和 OnResourceExecuted。

        /* ResourceExecutedContext 是 ASP.NET Core 中的一个类，它提供了有关资源执行的上下文信息和结果。

        ResourceExecutedContext 类包含以下属性和方法：

        ActionDescriptor：获取正在执行的动作方法的描述信息。
        CancellationToken：获取用于取消操作的取消令牌。
        Exception：如果在资源执行期间发生异常，则获取该异常的引用。
        HttpContext：获取当前的 HTTP 上下文。
        ModelState：获取模型状态字典，该字典包含在资源执行期间收集的模型验证错误。
        Result：获取或设置资源执行的结果。
        ResultHandled：获取或设置一个值，指示是否已处理结果。
        ResultWasExecuted：获取一个值，指示资源是否已执行结果。
        RouteData：获取路由数据，其中包含与资源执行相关的路由信息。
        ServiceLocator：获取或设置服务定位器实例，用于解析依赖项。
        StopProcessing：获取或设置一个值，指示是否停止后续过滤器和资源执行。

        通过使用 ResourceExecutedContext 对象，你可以访问和操作资源执行的结果、
        HTTP 上下文、异常信息、模型状态等，以及控制后续过滤器和资源执行的行为。 */

    {
        /// <summary>
        /// 在XX资源之后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("CustomResourceFilterAttribute.OnResourceExecuted");
        }

        //执行顺序：OnResourceExecuting方法-->控制器构造函数-->API方法-->OnResourceExecuted方法

        /// <summary>
        /// 在XX资源之前
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("CustomResourceFilterAttribute.OnResourceExecuting");
        }
    }
}
