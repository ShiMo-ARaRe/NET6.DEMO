//这个命名空间包含了处理配置选项的类和接口。IOptions<T> 接口和 Options<T> 类是用于访问和管理配置选项的常用类型。
using Microsoft.Extensions.Options;
//这个命名空间包含了处理身份验证和令牌相关的类和接口。其中包括生成和验证 JSON Web Tokens (JWT) 的类和算法。
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

//这个命名空间包含了处理 JWT 的类和工具。JwtSecurityTokenHandler 类是用于生成和验证 JWT 的主要类。
using System.IdentityModel.Tokens.Jwt;
//这个命名空间提供了对声明（Claims）的支持。声明是一种用于在身份验证和授权场景中传递有关用户的信息的数据结构。
using System.Security.Claims;
//这个命名空间提供了各种加密算法和密码学操作的类和接口。在这里，它可能被用于生成密钥和进行加密操作。
using System.Security.Cryptography;
//这个命名空间包含了用于处理字符串和字符的类和方法。
using System.Text;

namespace NET6.Demo.IdentitySer.Utility
{
    public class CustomRSSJWTervice : ICustomJWTService
    {
        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions; //这个字段用于存储 JWT Token 的选项配置。
        public CustomRSSJWTervice(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            /*  IOptionsMonitor<JWTTokenOptions> 接口用于获取配置选项的当前值，
                具体来说是获取JWTTokenOptions配置选项的当前值。(配置选项的值来自于appsettings.json文件）
                在构造函数中，通过jwtTokenOptions.CurrentValue获取了JWTTokenOptions配置选项的当前值，
                并将其赋值给_JWTTokenOptions字段。   */
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;

            // 注意在非对称可逆加密中我们没有用到秘钥(SecurityKey),我们用的是私钥和公钥。
        }

        /// <summary>
        /// 返回token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetToken(CurrentUser user)
        {

            #region 使用加密解密Key  非对称 
            //获取当前应用程序的工作目录，并将其赋值给 keyDir 变量。这个工作目录将用作密钥文件的保存目录。
            string keyDir = Directory.GetCurrentDirectory();

            /*  TryGetKeyParameters 方法接受三个参数：密钥目录、一个布尔值和一个 out 参数。
                它尝试从指定的密钥目录中获取 RSA 密钥参数，并将结果赋值给 keyParams 参数。*/
            /*  ref 参数用于双向数据传递，可以在方法内外修改。
                out 参数用于从方法内部传递值回调用者，不需要在方法调用之前初始化。*/
            if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false) 
                //  == false它用于判断是否成功获取了 RSA 密钥参数。
            {
                //调用 RSAHelper 类的 GenerateAndSaveKey 方法，生成新的 RSA 密钥并保存到指定的密钥目录中。
                //生成的密钥参数将赋值给 keyParams 变量。
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir); // 返回的是私钥
            }
            /*
                上面这段代码的作用是检查指定的密钥目录是否存在有效的 RSA 密钥。
                如果密钥不存在或无效，它会生成新的密钥并保存到指定的目录中。
                最终，keyParams 变量将包含有效的 RSA 密钥参数，无论是从现有文件获取还是生成的新密钥。*/
            #endregion

            //准备有效载荷
            List<Claim> claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name, user.Name),
                 new Claim("NickName",user.NikeName),
                 new Claim("Description",user.Description),
                 new Claim("Age",user.Age.ToString()),
            };
            foreach (var role in user.RoleList)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

           /* 一般情况下，使用私钥进行签名生成 JWT（JSON Web Token），而使用公钥进行验证和解析 JWT。

            生成 JWT 需要使用私钥对 JWT 的内容进行签名，以确保 JWT 的完整性和身份验证。私钥是用于生成签名的关键，它是只有持有者知道的机密信息。生成 JWT 的过程称为签名。

            验证和解析 JWT 时，需要使用公钥来验证 JWT 的签名是否有效，以确保 JWT 的来源和完整性。公钥用于验证签名，它是公开的并可供其他人使用。*/

            //准备加密key
            //使用 RSA 密钥参数 keyParams 创建了一个 RsaSecurityKey 实例。这个实例用于表示 JWT 的签名密钥。
            RsaSecurityKey key = new RsaSecurityKey(keyParams);

            //Sha256 加密方式
            /*  使用上一步创建的 RsaSecurityKey 实例 key 和指定的签名算法 SecurityAlgorithms.RsaSha256Signature 创建
                了一个 SigningCredentials 实例 creds。SigningCredentials 表示 JWT 的签名凭证。*/
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
            // 使用了公钥和私钥对 JWT 进行签名和验证。在非对称加密中，私钥用于生成签名，公钥用于验证签名。

            JwtSecurityToken token = new JwtSecurityToken(
                //在这个实例中，指定了 JWT 的发行者（issuer）、受众（audience）、声明（claims）、过期时间（expires）和签名凭证（signingCredentials）
                  issuer: _JWTTokenOptions.Issuer,
                  audience: _JWTTokenOptions.Audience,
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(5),//5分钟有效期
                  signingCredentials: creds);

            //使用 JwtSecurityTokenHandler 类的 WriteToken 方法将 JWT token 转换为字符串表示，并将其作为方法的返回值。
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
