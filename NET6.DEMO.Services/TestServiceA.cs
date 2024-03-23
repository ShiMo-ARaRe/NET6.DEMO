using NET6.DEMO.Interfaces;

//不再关注具体

namespace NET6.DEMO.Services
{
    public class TestServiceA : ITestServiceA
    {

        public TestServiceA() // 构造函数
        {
            //GetType().Name 表达式会返回当前对象的类型名称
            Console.WriteLine($"{GetType().Name} 被构造~~");
        }

        public string ShowA()
        {
            //GetType().FullName 表达式会返回当前对象的类型的完全限定名称
            return $"this is from {GetType().FullName} ShowA";
        }
    }
}