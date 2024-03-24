namespace NET6.DEMO.Models
{
    /// <summary>
    /// 学员
    /// </summary>
    public class Student
    { 
        /// <summary>
        /// 学员Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学员名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 学员年龄
        /// </summary>
        public int Age { get; set; }
    }
}