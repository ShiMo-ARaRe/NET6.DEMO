using NET6.DEMO.Interfaces;

//不再关注具体

namespace NET6.DEMO.Services
{
    public class TestServiceB : ITestServiceB
    {
        public ITestServiceA _TestServiceA;
        public TestServiceB(ITestServiceA testServiceA)
        {
            _TestServiceA = testServiceA;
            //GetType().Name 表达式会返回当前对象的类型名称
            Console.WriteLine($"{GetType().Name} 被构造~~");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ShowB()
        {
            //GetType().FullName 表达式会返回当前对象的类型的完全限定名称
            return $"this is from {GetType().FullName} ShowB  调用 {_TestServiceA.ShowA()}";
        }
    }
}