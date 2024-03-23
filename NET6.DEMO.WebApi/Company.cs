namespace NET6.DEMO.WebApi
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string? AddRess { get; set; }
    }
}
