//引用了Microsoft.Extensions.Options命名空间，用于使用IOptionsMonitor<T> 接口来获取配置选项的当前值。
using Microsoft.Extensions.Options;
//引用了Microsoft.IdentityModel.Tokens命名空间，用于使用身份验证和令牌相关的类和方法。
using Microsoft.IdentityModel.Tokens;
//引用了System.IdentityModel.Tokens.Jwt命名空间，用于使用JWT（JSON Web Token）相关的类和方法。
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;

//引用了System.Security.Claims命名空间，用于处理声明（Claims）相关的类和方法。
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;


//引用了System.Text命名空间，用于处理字符串编码相关的类和方法。
using System.Text;

namespace NET6.Demo.IdentitySer.Utility
{
    public class CustomHSJWTService : ICustomJWTService
    {

        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        //构造函数使用依赖注入将IOptionsMonitor<JWTTokenOptions> 类型的实例注入到类中。
        public CustomHSJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            /*  IOptionsMonitor<JWTTokenOptions> 接口用于获取配置选项的当前值，
                具体来说是获取JWTTokenOptions配置选项的当前值。(配置选项的值来自于appsettings.json文件）
                在构造函数中，通过jwtTokenOptions.CurrentValue获取了JWTTokenOptions配置选项的当前值，
                并将其赋值给_JWTTokenOptions字段。   */
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        #endregion

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetToken(CurrentUser user)
            //方法接收一个CurrentUser对象作为参数，用于 "构建" JWT令牌的声明（Claims）。
        {
            //准备有效载荷

            //在方法中，根据CurrentUser对象的属性构建了一组声明（Claims）。
            List<Claim> claims = new List<Claim>()
            {
                /*  使用预定义的常量（如 ClaimTypes.Name）来表示声明的类型，
                    有助于确保在不同的身份验证和授权系统之间的一致性，以及简化代码的编写和维护。*/
                 new Claim(ClaimTypes.Name, user.Name),
                 new Claim("NickName",user.NikeName), 
                 new Claim("Description",user.Description),
                 new Claim("Age",user.Age.ToString()),
            }; 
            foreach (var role in user.RoleList)
            {
                /*  在循环的每次迭代中，使用Claim类创建一个新的声明对象，并将其添加到claims列表中。
                    Claim类的构造函数接收两个参数，第一个参数表示声明的类型（在这里使用了ClaimTypes.Role，
                    表示角色类型的声明），第二个参数表示声明的值（在这里使用了role，表示具体的角色值）。*/
                claims.Add(new Claim(ClaimTypes.Role, role));
                /* ClaimTypes.Role是一个预定义的声明类型，用于表示用户的角色信息。
                它是一个标准化的声明类型，广泛用于身份验证和授权场景，
                以确保在不同系统之间共享和使用角色声明时的一致性。 */
            }


            //需要安装Microsoft.IdentityModel.Tokens和System.IdentityModel.Tokens.Jwt包

            //准备加密key
            /* 创建了一个SymmetricSecurityKey对象，使用UTF - 8编码将配置选项
            _JWTTokenOptions.SecurityKey转换为字节数组，作为JWT令牌的加密密钥。*/
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

            //Sha256 加密方式
            /* 创建了一个SigningCredentials对象，将上述创建的加密密钥和加密算法（HMACSHA256）
            传递给它用于对JWT令牌进行签名。*/
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            /*  JWT 是一种特定类型的令牌，它采用 JSON 格式，包含了一组声明（claims）作为令牌的内容。
                这些声明可以包括用户的身份信息、权限、过期时间等。
                JWT 通常由三部分组成：头部（Header）、载荷（Payload）和签名（Signature）。
                头部（Header）包含了关于令牌的元数据和加密算法的信息。
                载荷（Payload）包含了实际的声明信息，例如用户的身份、角色、权限等。
                签名（Signature）用于验证令牌的完整性和真实性，确保令牌在传输过程中没有被篡改。*/


            //创建了一个JwtSecurityToken对象,用于表示JWT令牌。
            JwtSecurityToken token = new JwtSecurityToken(
                  issuer: _JWTTokenOptions.Issuer, // 发行者
                  audience: _JWTTokenOptions.Audience,// 受众
                  claims: claims, // 声明（Claims）
                  expires: DateTime.Now.AddDays(7),//过期时间 //7天有效期,
                  signingCredentials: creds); // 签名凭据等参数

            //最后使用JwtSecurityTokenHandler类的WriteToken方法将JWT令牌序列化为字符串，并将其作为方法的返回值。
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
