//这段代码是一个名为 RSAHelper 的C#类，用于处理RSA加密算法相关的操作。

/* Newtonsoft.Json 是一个流行的第三方库，用于在 C# 中处理 JSON 数据。
 * 它提供了一组强大的工具和方法，使得在 C# 中序列化、反序列化和操作 JSON 数据变得更加简单和方便。*/
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json; // 需要安装Newtonsoft.Json包
/*这是引入 .NET Framework 中的 System 命名空间的语句。System 命名空间包含了许多核心的类和类型，
 * 提供了基本的系统功能和数据类型，例如字符串、日期时间、异常处理等。
 * 通过引入 System 命名空间，你可以在代码中使用其中定义的类和类型。*/
using System;
/* System.Collections.Generic 命名空间包含了各种用于集合（如列表、字典等）操作的泛型类和接口。
 * 通过引入该命名空间，你可以使用泛型集合来存储和操作数据。*/
using System.Collections.Generic;
using System.IO;    // System.IO 命名空间提供了访问文件和流的类和方法，用于文件读写、目录操作等。
/* System.Linq 命名空间提供了一组用于查询和操作集合数据的扩展方法。
 * 通过引入该命名空间，你可以使用 LINQ（Language Integrated Query）语法来对集合进行
 * 查询、过滤、排序和转换等操作。*/
using System.Linq;
/* System.Security.Cryptography 命名空间提供了各种加密和哈希算法的实现，
 * 用于数据的加密、解密和数据完整性校验等安全操作。*/
using System.Security.Cryptography; // RSAParameters
using System.Threading.Tasks;   // 提供了用于多线程编程和异步操作的类和方法。

namespace NET6.Demo.IdentitySer.Utility
{
    public class RSAHelper
    {
        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <param name="withPrivate"></param>
        /// <param name="keyParameters"></param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters keyParameters)
        /*
         TryGetKeyParameters 方法用于从本地文件中读取用于签发 Token 的 RSA 密钥参数。
        filePath 表示存放密钥的文件夹路径
        withPrivate 表示是否包含私钥
        keyParameters 是一个 out 参数，用于返回读取到的密钥参数。
         */
        {
            //key.json 表示包含私钥的文件，key.public.json 表示包含公钥的文件。
            string filename = withPrivate ? "key.json" : "key.public.json"; // 传过来的withPrivate始终为真，因为要拿私钥
            //使用 Path.Combine() 方法将文件路径 filePath 和文件名 filename 组合成完整的文件路径 fileTotalPath。
            string fileTotalPath = Path.Combine(filePath, filename);
            //将 keyParameters 变量设置为默认值。
            //这是为了确保在文件不存在的情况下，keyParameters 变量具有一个有效的初始值。
            keyParameters = default;

            if (!File.Exists(fileTotalPath)) //通过 File.Exists() 方法检查文件是否存在。如果文件不存在，返回 false；
            {
                return false;
            }
            else
            {
                //使用 File.ReadAllText() 方法读取文件的所有文本内容，并使用 JsonConvert.DeserializeObject() 方法
                //将文本内容反序列化为 RSAParameters 对象，并将结果赋值给 keyParameters 变量。
                keyParameters = JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(fileTotalPath)); // 拿私钥
                return true;
            }
        }

        //公钥是用于加密和验证数字签名的密钥之一。
        //它可以被广泛地分发和公开，因此任何人都可以使用公钥对数据进行加密或验证数字签名。
        /*
            私钥是与公钥配对的密钥，是加密和数字签名过程中的另一个关键组成部分。
            私钥是保密的，并且只有私钥的持有者可以使用它来解密加密的数据或生成数字签名。
            私钥用于解密使用公钥加密的数据，以及用于对数据进行数字签名。
            私钥的安全性非常重要，只有私钥的合法持有者才能访问和使用私钥。
         */
        /*  虽然公钥和私钥是一一对应的
            但从已知的公钥是无法直接推导出私钥的。在常见的非对称加密算法中，如RSA和ECC，
            从公钥推导出私钥是一个计算上的困难问题，被称为“密钥推导”或“私钥恢复”问题。
            这种困难性是基于数学上的复杂性和算法安全性的原理。
         */

        /// <summary>
        /// 生成并保存 RSA 公钥与私钥
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <returns></returns>
        public static RSAParameters GenerateAndSaveKey(string filePath, bool withPrivate = true)
        //GenerateAndSaveKey 方法用于生成并保存 RSA 公钥和私钥。
        //它接受两个参数：
        //filePath 表示存放密钥的文件夹路径
        //withPrivate 表示是否生成包含私钥的密钥对，默认为 true。
        {
            //声明了两个变量 publicKeys 和 privateKeys，用于存储生成的公钥和私钥参数。
            RSAParameters publicKeys, privateKeys;

            //using 块用于确保在使用完密钥对后正确释放相关资源。
            using (var rsa = new RSACryptoServiceProvider(2048))//即时生成
                // RSA 密钥的生成是根据随机数和特定的算法生成的。
                //使用 RSACryptoServiceProvider 类创建一个指定长度为 2048 的 RSA 密钥对。
            {
                try
                {
                    //将生成的 RSA 密钥对中的私钥参数导出并赋值给 privateKeys 变量。
                    privateKeys = rsa.ExportParameters(true);
                    //将生成的 RSA 密钥对中的公钥参数导出并赋值给 publicKeys 变量。
                    publicKeys = rsa.ExportParameters(false);
                }
                //在 finally 块中执行最后的清理工作。
                finally
                {
                    //将 PersistKeyInCsp 属性设置为 false，表示不在密钥容器中保留密钥。
                    rsa.PersistKeyInCsp = false;
                }
            }

            //使用 File.WriteAllText() 方法将私钥参数和公钥参数分别以 JSON 格式保存到指定的文件中。
            File.WriteAllText(Path.Combine(filePath, "key.json"), JsonConvert.SerializeObject(privateKeys));

            File.WriteAllText(Path.Combine(filePath, "key.public.json"), JsonConvert.SerializeObject(publicKeys));
            return withPrivate ? privateKeys : publicKeys; // 默认生成私钥，返回私钥
        }
    }
}
