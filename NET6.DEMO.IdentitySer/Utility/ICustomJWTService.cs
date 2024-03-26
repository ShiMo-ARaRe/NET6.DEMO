namespace NET6.Demo.IdentitySer.Utility
{
    public interface ICustomJWTService
    {

        /// <summary>
        /// 就是专门来办法Token的
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GetToken(CurrentUser user);
    }
}
