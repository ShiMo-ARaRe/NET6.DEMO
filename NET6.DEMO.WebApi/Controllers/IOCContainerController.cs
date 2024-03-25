//关于依赖注入和控制反转（Inversion of Control，IoC）容器的概念。


//引入了 ASP.NET Core MVC 的命名空间，其中包含了用于创建 Web 应用程序的控制器、动作结果、过滤器等类和特性。
using Microsoft.AspNetCore.Mvc;
//引入了 NET6.DEMO.Interfaces 命名空间，其中包含了自定义的接口类型的定义。通过引入此命名空间，可以在代码中使用这些接口类型。
using NET6.DEMO.Interfaces;
//引入了 NET6.DEMO.WebApi.Utility.Swagger 命名空间，其中包含了与 Swagger 相关的工具类和特性。
//Swagger 是一种用于设计、构建、文档化和消费 Web API 的工具，
//该命名空间提供了在 ASP.NET Core Web API 项目中使用 Swagger 的工具和功能。
using NET6.DEMO.WebApi.Utility.Swagger;

//using NET6.DEMO.Services; // 这是传统做法，现在我们用IOC容器来实现
/* 传统做法：

在传统的做法中，为了获取所需的服务实例，通常会直接使用 using 语句引入具体的命名空间，
例如 NET6.DEMO.Services。然后，通过实例化该命名空间下的类来获取服务的实例。
这种做法存在一些问题，例如类之间的依赖关系较强，难以进行解耦和测试。
同时，如果需要在不同的地方更换或替代具体的服务实现，需要修改大量的代码。

使用 IoC 容器：

现在的做法是使用控制反转（IoC）容器来管理和解决对象之间的依赖关系。
IoC 容器是一种实现依赖注入的机制，它负责创建和管理对象的生命周期，
并将所需的服务实例注入到需要的地方，从而实现对象之间的解耦和灵活性。
通过使用 IoC 容器，不再需要在代码中直接实例化具体的服务类，而是将依赖关系的配置交给容器来处理。
容器会根据配置和约定，自动解析和创建所需的服务实例，并将其注入到需要的地方。
使用 IoC 容器可以提高代码的可维护性、可测试性和可扩展性，而且可以更方便地切换和替换具体的服务实现。
因此，这句注释中的内容表达了一种在现代开发中推崇的做法，即使用 IoC 容器来管理依赖关系，
而不是直接引入具体的命名空间并实例化服务类。这样可以实现更松散的耦合、更灵活的替换和更好的代码可维护性。
 */


namespace NET6.DEMO.WebApi.Controllers // 定义命名空间
{
    [ApiController] // 标记这是一个API控制器
    [ApiVersion("1.0")] // 指定API版本为1.0
    [Route("[controller]/v{version:apiVersion}")] // 定义路由模板
    public class IOCContainerController : ControllerBase // 定义一个名为IOCContainerController的类，继承自ControllerBase
    {
        private readonly ILogger<IOCContainerController> _logger; // 定义一个ILogger<IOCContainerController>类型的私有只读字段_logger
        private readonly ITestServiceA _ITestServiceA; // 定义一个ITestServiceA类型的私有只读字段_ITestServiceA
        private readonly ITestServiceB _ITestServiceB; // 定义一个ITestServiceB类型的私有只读字段_ITestServiceB

        //IServiceProvider 是 ASP.NET Core 中的一个接口，它定义了一个用于获取服务实例的机制。
        //它是依赖注入（Dependency Injection）的核心接口之一。
        private readonly IServiceProvider _IServiceProvider;


        // 定义构造函数 // 在这个构造函数上打断点看看构造过程
        public IOCContainerController(ILogger<IOCContainerController> logger, ITestServiceA iTestServiceA, ITestServiceB iTestServiceB, IServiceProvider iServiceProvider)
        /* 比如每次请求ITestServiceA时，都创建一个新的TestServiceA实例。
         所以，当IOCContainerController的构造函数需要一个ITestServiceA实例时，
         依赖注入容器就会自动创建一个TestServiceA实例，并将其注入到构造函数中。*/

        // 注意：【内置容器仅支持构造函数注入】，所以下面的ShowA方法需要用到IServiceProvider

        {
            _logger = logger; 
            _ITestServiceA = iTestServiceA; 
            _ITestServiceB = iTestServiceB; 
            _IServiceProvider = iServiceProvider;
            // 接口本身不能被实例化。但在依赖注入模式中，我们通常会注册一个接口到其实现类的映射。
            // 这样，当接口被注入到其他类中时，依赖注入容器会自动创建其实现类的实例。
        }

        [HttpGet()] // 标记这是一个HTTP GET方法
        public string ShowA([FromServices] ITestServiceB iTestServiceBNew, [FromServices] IServiceProvider iServiceProvider)
        // 该方法的参数中使用了[FromServices] 属性，它表示这两个参数的值将通过依赖注入容器进行注入。
        // 也就是说，当调用ShowA方法时，ASP.NET Core会自动从依赖注入容器中获取ITestServiceB和IServiceProvider的实例，
        // 并传递给ShowA方法。

        {

            // GetService表示获取服务，比如<>里面写上ITestServiceB接口，就能返回新的TestServiceB实例
            ITestServiceB testServiceB1 = _IServiceProvider.GetService<ITestServiceB>()!; 
            // 从_IServiceProvider（这是本控制器中的字段）中获取ITestServiceB类型的服务实例，赋值给testServiceB1
            
            // 上面代码和下面代码效果完全一样。

            ITestServiceB testServiceB2 = iServiceProvider.GetService<ITestServiceB>()!;
            // 从iServiceProvider（这是当前方法中新获取的）中获取ITestServiceB类型的服务实例，赋值给testServiceB2

            /* TestServiceB类依赖于TestServiceA类
            ITestServiceA的实例是通过TestServiceB的构造函数参数传入的。这是依赖注入的一种方式，称为构造函数注入。
            当创建TestServiceB的实例时，依赖注入容器会自动创建一个ITestServiceA的实例（即一个TestServiceA对象）
           （具体的实现类取决于在依赖注入容器中如何注册ITestServiceA接口的），并将其传入TestServiceB的构造函数。
            */

            /*关于!号
            在你的代码_IServiceProvider.GetService<ITestServiceB>()!中，GetService<ITestServiceB>()方法可能
            会返回null（如果没有找到对应的服务）。但是通过在其后面添加!，你告诉编译器你确信它不会为null，
            因此编译器不会为此产生警告。
            */

            // 纯纯用于演示，没什么事务逻辑。
            //return _ITestServiceA.ShowA(); // 调用_ITestServiceA的ShowA方法，并返回结果 // 这也是控制器中已有的字段
            return testServiceB2.ShowB(); // 调用testServiceB2的ShowB方法，并返回结果

        }

        [HttpPost()] // 标记这是一个HTTP POST方法
        public string ShowB() // 定义一个名为ShowB的方法，返回值类型为string
        {
            return _ITestServiceB.ShowB(); // 调用_ITestServiceB的ShowB方法，并返回结果
        }
    }
}
