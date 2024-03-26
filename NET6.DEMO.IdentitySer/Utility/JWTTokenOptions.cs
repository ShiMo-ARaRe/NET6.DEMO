namespace NET6.Demo.IdentitySer.Utility
{

    /* JWTTokenOptions类: 这是一个包含JWT令牌选项的类，用于将配置文件中的JWTTokenOptions部分的值映射到该类的属性。
    Audience属性: 用于获取或设置JWT令牌的受众（audience）。
    Issuer属性: 用于获取或设置JWT令牌的发行者（issuer）。
    SecurityKey属性: 用于获取或设置JWT令牌的安全密钥（security key）。 */
    public class JWTTokenOptions
    {
        public string? Audience { get; set; }

        /// <summary>
        /// 加密key
        /// </summary>
        public string? SecurityKey { get; set; }


        public string? Issuer { get; set; }
    }
}
