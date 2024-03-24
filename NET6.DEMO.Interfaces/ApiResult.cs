using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET6.DEMO.Interfaces
{
    public class ApiResult<T> where T : class
        //where T : class是一个类型约束，它限制了T必须是引用类型。
        //这样做的目的是确保T不是值类型（例如int、bool等），而是一个可以为null的引用类型。
    {
        /// <summary>
        /// Api执行是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? Message { get; set; } // 返回值可以为null

        /// <summary>
        /// 结果集
        /// </summary>
        public T? Data { get; set; }
    }
}
