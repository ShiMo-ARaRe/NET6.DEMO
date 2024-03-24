using NET6.DEMO.Interfaces;
using NET6.DEMO.Models;
using System.Xml.Linq;

namespace NET6.DEMO.Services
{
    /// <summary>
    /// 学员类实现
    /// </summary>
    public class StudentService : IStudentService
    {

        //IAsyncEnumerable<Student> 表示该方法返回一个能够以异步方式迭代 Student 对象的集合。（异步枚举器
        public async IAsyncEnumerable<Student> GetUserListAsync()
            //async 表示该方法是异步的，可以在执行过程中暂停和恢复。
        {
            for (int i = 0; i < 50; i++)
            {
                //使用 Task.Delay 方法以异步方式暂停当前方法的执行，模拟一定的延迟。这里延迟时间为 100 毫秒。
                await Task.Delay(100);
                //使用 yield return 语句生成一个新的 Student 对象，并将其作为异步枚举器的下一个元素返回。
                yield return new Student()
                /*使用 yield return 语句的方法被称为迭代器方法。
                使用 yield return 的迭代器方法提供了一种简洁、延迟执行的方式来生成集合的元素。
                它避免了一次性生成整个集合，而是按需生成每个元素，可以提高性能和节省内存。*/
                {
                    Id = i,
                    Name = $"学员-{i}",
                    Age = i,
                };
                //在每次循环迭代时，都会生成一个新的学员对象，每个学员对象具有不同的 Id、Name 和 Age 属性。
            }

            /*  异步方法的执行可以在遇到 await 关键字时暂停，然后在等待的操作完成后恢复执行。
                这样可以避免阻塞线程，提高应用程序的并发性和响应性。
                同步方法会按照顺序逐个生成学员对象，期间无法进行其他操作。
                而异步方法可以利用 await 的特性，在等待的过程中释放线程，
                使得线程可以执行其他任务，提高了应用程序的并发性。*/
        }

        public bool Validata(string nickname)
        {
            //使用 Equals 方法比较传入的 nickname 字符串是否等于 "金牌讲师Richard老师"。
            return  nickname.Equals("金牌讲师Richard老师");
        }
    }
}