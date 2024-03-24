using NET6.DEMO.Models;

namespace NET6.DEMO.Interfaces 
{
    public interface IStudentService
    {
        /// <summary>
        /// 学员类抽象
        /// </summary>
        /// <returns></returns>
        public IAsyncEnumerable<Student> GetUserListAsync();
        //IAsyncEnumerable<T> 是 C# 8.0 引入的异步枚举器接口，它允许以异步的方式迭代集合的元素。


        public bool Validata(string nickname);
        
    }
}