//����Ĵ�������������ASP.NET CoreӦ�ó����HTTP������ܵ��ͷ��������� // ��������

//using static �� C# 6.0 �����һ���﷨�����������ڴ�����ֱ��ʹ����ľ�̬��Ա��������ʹ��������Ϊ�޶�����
using static System.Net.Mime.MediaTypeNames; //�� .NET Framework �ṩ��һ����
//��������һϵ�г��õ�ý���������ƣ�media type names������Щý��������������ָ�����ֻ�����ý�����ͣ�
//�����ı���ͼ����Ƶ����Ƶ�ȡ�ͨ��ʹ�� MediaTypeNames ���еľ�̬��Ա�������ֱ��������Щý���������ƣ��������ֶ������ַ�����

using System.Diagnostics; //�����ռ��ṩ��һ�����ڴ���Ϳ��ƽ��̡��¼���־�����ܼ��������ࡣ
using NET6.DEMO.WebApi.Utility.Swagger; // �汾���ƣ�����汾��
using Microsoft.OpenApi.Models; // �����������ڱ�ʾ�͹��� OpenAPI �淶����ͽӿڡ�

using System.Text.Encodings.Web;
using System.Text.Unicode; // �����������ռ��ṩ�������� Unicode ��صĹ��ܡ�// ����Ŀ����Ϊ�˽��������������

using NET6.DEMO.WebApi.Utility.Route; // ȫ��·����չ
using Microsoft.AspNetCore.Mvc; //����һ�������ռ����ã���ʾ������ʹ����ASP.NET Core��MVC��ܡ�

using NET6.DEMO.WebApi.Utility.Version; // ���ð�����������Api֧�ְ汾

//�߱����󡾽ӿںͳ����ࡿ��ʵ�֡���ͨ�ࡿ
using NET6.DEMO.Interfaces; // ����
using NET6.DEMO.Services; // ����

using NLog.Web;
using NLog; // ����NLog����������־

using NET6.DEMO.WebApi.Utility.Filters; // ���������

using Microsoft.AspNetCore.Authorization;// �������ռ����һЩ���ڽ��������֤����Ȩ����ͽӿڡ�
using NET6.Demo.WebApi;// Ϊ�˷�����JWTTokenOptions��
//���ڴ��� JWT��JSON Web Token�������֤��һ�������ð��ṩ���������úʹ��� JWT �����֤���������м����
using Microsoft.AspNetCore.Authentication.JwtBearer;
/*�ṩ���� ASP.NET Core �� .NET Ӧ�ó����д���ȫ���ƺͼ�����Կ�Ĺ��ܡ�
��������һЩ��ͷ������������ɡ���֤�ʹ���ȫ�����Լ����������Կ��*/
using Microsoft.IdentityModel.Tokens;
using System.Text; // �ṩ�����ڴ����ı�������ַ�����������ͷ�����
using System.Security.Claims; // ���ڴ��������֤����Ȩ�����������Claims����
using NET6.Demo.WebApi.Utility;  //����ʵ�ù����࣬���һЩ���õĹ��ܺ͹�����չ������߿���Ч�ʺʹ���Ŀ�ά���ԡ�


// ��ȡNLog�����ļ�
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("CfgFile/NLog.config").GetCurrentClassLogger();

//����һ��Ӧ�ó��򹹽���builder���ù������������ú͹���Ӧ�ó���
WebApplicationBuilder builder = WebApplication.CreateBuilder(args); // ��var�Զ��ƶ�����Ҳ��

//����log4net (��Ҫ���� log4net + Microsoft.Extensions.Logging.Log4Net.AspNetCore��
//builder.Logging.AddLog4Net("CfgFile/log4net.Config"); // ʹ�ú���滻�����õ���־

#region NLog����  (��Ҫ����NLog.Web.AspNetCore��
/*  ClearProviders() �����������Ӧ�ó������־�ṩ������ȷ���������µ���־
    �ṩ����֮ǰ������������־��¼������ӡ��������Ա�����Ĭ�ϵ���־�ṩ�����ͻ���ظ���־��¼�����⡣*/
//builder.Logging.ClearProviders();

/*  SetMinimumLevel() ��������������־��¼����С�������������־��¼��������Ϊ Information��
    ��ʾֻ��¼��Ϣ���𼰸��߼������־��Ϣ�����ͼ������־��Ϣ������ Debug���������ԡ�
    �������ڿ�����־����ϸ�̶ȣ��Ա����¼�����������Ϣ��*/
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);

/*  UseNLog() ������ʹ�� NLog ��־��������Ӧ�ó������־��¼��
    ���� NLog ��־�ṩ������ӵ�Ӧ�ó������־��¼���У��Ա�ʹ�� NLog ������־��¼��*/
builder.Host.UseNLog();
#endregion

// Add services to the container.// ��������ӵ������С�
//��������������ӵ�����ע�������С���������ʹӦ�ó����ܹ�ʹ��ASP.NET Core MVC������������ӦHTTP����
builder.Services.AddControllers(option => // ȫ������
{
    //option.Filters.Add<CustomExceptionFilterAttribute>(); //ȫ��ע�� --����������Ŀ�����еķ�������Ч��(�����쳣����չ

    //RouteAttribute �� ASP.NET Core �е�һ�����ԣ�Attribute����������ָ���������������·��ģ�塣
    //���Կ���Ӧ���ڿ��������������еĲ������������ڶ������ǵ�·��·����
    option.Conventions.Insert(0, new RouteConvention(new RouteAttribute("api/"))); // �����п��������·��ǰ׺
    // ��һ����������Ϊ0������0�ͻᱨ������ʾ��ӵ���ǰ�档
}
).AddJsonOptions(options => // ���������������
{
    //���Զ� JSON ���л�ѡ������Զ������á������������һ�� Lambda ���ʽ������ָ��������� JSON ѡ�
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

// �˽��й����� Swagger/OpenAPI �ĸ�����Ϣ������� https://aka.ms/aspnetcore/swashbuckle

//���ð�����������API�汾����
builder.ApiVersionExt(); // ʹ������������ǣ���ͬAPI�汾�Ŀ��������ֿ���һ��������ҲҪע�⣬���Ǳ��봦�ڲ�ͬ�ļ����£�

//����Swagger����������
builder.AddSwaggerGenExt(); // Ϊ�˽�ʡProgram�еĿռ䣬������߼���д��SwaggerExtension��ȥ��(��������з�װ��˼��

#region ע�����;���֮��Ĺ�ϵ
//��δ���ʹ��������ע��������ͨ���� ASP.NET Core �е������������� Services ������ע�����ĳ���;���֮��Ĺ�ϵ��
    builder.Services.AddTransient<ITestServiceA, TestServiceA>();
//AddTransient �������ڽ�����ע��Ϊ˲̬��Transient���������ڡ�
//˲̬�������ڱ�ʾÿ���������������������˼���ǣ���������ͼʵ�����ӿ�ʱ��ʱ���ᴴ��һ���µ�TestServiceAʵ����

builder.Services.AddTransient<ITestServiceB, TestServiceB>();
builder.Services.AddTransient<IStudentService, StudentService>();

/* Ϊ��֧��ServcieFilter
 �����ע��������������ô���޷�ͨ��[ServiceFilter(typeof(CustomLogActionFilterAttribute))]��
 [ServiceFilter(typeof(CustomLogActionFilterAttribute))]��������Ӧ���ڿ�������API������
 */
builder.Services.AddTransient<CustomAsyncExceptionFilterAttribute>();
builder.Services.AddTransient<CustomLogActionFilterAttribute>();

/* ��ASP.NET Core������ע��������ע��һ������
 * ����������������ҪIAuthorizationHandler�ӿڵ�ʵ��ʱ��ʹ��NikeNameAuthorizationHandler����д�����
 * ����ζ��NikeNameAuthorizationHandler�����ڴ�����Ȩ�߼���*/
builder.Services.AddTransient<IAuthorizationHandler, NikeNameAuthorizationHandler>();
#endregion

#region ��Ȩ����Ȩ
// ��Ҫ��װMicrosoft.AspNetCore.Authentication.JwtBearer��

JWTTokenOptions tokenOptions = new JWTTokenOptions(); // ����ʵ��
// �������ļ��е� "JWTTokenOptions" �ڵ��ֵ�󶨵� tokenOptions ����
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
builder.Services
  .AddAuthorization(option =>
  //ͨ��builder.Services.AddAuthorization��������Ȩ������ӵ�Ӧ�ó���ķ��������С�
  {
      /*    ��ɫ��
           "admin",
           "teacher",
           "student" */

      //ContainsNickname ��������
      option.AddPolicy("Policy001", policyBuilder =>
      //ʹ��option.AddPolicy����������һ����Ϊ"Policy001"����Ȩ���ԡ������Ȩ���԰����˶����ȨҪ��
      {
          //�������ʲô
          //��Ȩ����Ҫ���û��������"admin"��ɫ���û���Ϊ"FFFF"�����ұ��������Ϊ"NickName"��������Claim����
          policyBuilder.RequireRole("admin");
          policyBuilder.RequireUserName("FFFF");
          policyBuilder.RequireClaim("NickName");

          /*  ��������еĻ���Ҫ����֤ʧ�ܣ������û�û�� "admin" ��ɫ���û������� "FFFF" ��
              û����Ϊ "NickName" �������������󽫱��ܾ���*/

          policyBuilder.RequireAssertion(context =>
          /* ʹ��policyBuilder.RequireAssertion����������һ���Զ������Ȩ���ԡ���Ȩ������һ��ί�У�
          ������һ��AuthorizationHandlerContext���󣨷�װ����Ȩ�������������������Ϣ����Ϊ������
          ������һ������ֵ��ʾ��Ȩ�Ƿ�ͨ����*/
          {
              //������������ܶ���߼��ж�
              //�û����������Ϊ"admin"�Ľ�ɫ������
              //�û��ĵ�һ����ɫ������ֵ����Ϊ"admin"��
              //�û���������κ�����Ϊ"ClaimTypes.Name"��������
              bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role) // һ��Ϊ��
                 && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "admin" // ����Equals������ == Ҳ��
                 && context.User.Claims.Any(c => c.Type == ClaimTypes.Name); // һ��Ϊ��

              /* ��Щ���������������Ϊclaims��������List<Claim>������֮ǰ��һ���û�����Ϣ��ֳ��˶�ݣ�
                 �����CustomHSJWTService.cs�ļ��� ׼����Ч�غ� ����*/

              /* ����������
                 ���Ժ�������������صĸ�����������û��������������������������֤����Ȩ������Я������֤��Щ������Ϣ��
                 �������������Զ�Ӧ�����Ե����ͣ�ֵ���Զ�Ӧ�����Եľ���ֵ��
                 ���Ը�������ͨ�õı�̸���Ͷ����������������������������֤����Ȩ���ض������У�����Я������֤�û���������Ϣ��
               */

              /* ����c.Type
                c.Type ��ʾ�������������ԣ�Type������ָ���������ĺ�������
              �������֤����Ȩ���������У����������������ֲ�ͬ���͵������������ɫ������������ȡ�

                Claim ����ͨ�����ж�����ԣ����� Type ���Դ������������ͣ��� Value ���Դ��������ľ���ֵ��
              ͨ������ c.Type ���ԣ����ǿ��Ի�ȡ�������� c ���������Ե�ֵ��(ע�ⲻ������ֵ���������Ե�ֵ����������Խ�ʲô��

                ������ض��ı��ʽ�У����ǽ��������� c ������������ ClaimTypes.Role ���бȽϡ�
              ClaimTypes.Role �� System.Security.Claims.ClaimTypes ���ж����һ����������ʾ��ɫ���͵�������*/

              /* HasClaim��Any��First
                HasClaim �� User ����ķ�����ֱ���� User �����ϵ��ã��� Claims.Any �� Claims ���Եķ�����
                ��Ҫ�ȷ��� User.Claims ��ȡ�û����������ϣ�Ȼ����� Any ������
                ����ֵ������ bool ���͡�����û������������д�������һ�������������������򷵻� true�����򷵻� false��
                ���ǵķ���ֵ���������ǵȼ۵ģ����ĸ���ʹ�ó����͸���ϲ�á�

                User.Claims.First �����ڻ�ȡ�û����������е�һ������ָ�������������ķ�����
                ��ͨ��.First(c => c.Type.Equals(ClaimTypes.Role))�õ�����/��������ͨ��.Valueȡֵ
                ����ֵ�����ص�һ��������������������
               */

              return bResult;
          });

      });

      option.AddPolicy("Policy002", policyBuilder =>
      {
          policyBuilder.AddRequirements(new CustomNickNameRequirement()); //������Ȩ��չ���Ѵ����߼��ŵ������ļ���
      });


      //option.AddPolicy("Policy002", policyBuilder =>
      //{
      //    //�������ʲô
      //    policyBuilder.RequireRole("admin");
      //    policyBuilder.RequireUserName("FFFF");
      //    policyBuilder.RequireClaim("NickName");

      //    policyBuilder.RequireAssertion(context =>
      //    /*    context ��һ�� AuthorizationHandlerContext ��������װ����Ȩ�������������������Ϣ��
      //          AuthorizationHandlerContext �ṩ�˷����û���Ϣ����Դ��Ϣ�Լ�������Ȩ��ص����ݵ�������
      //          context.User �� AuthorizationHandlerContext �����һ�����ԣ�����ʾ��ǰ������û������Ϣ��
      //          context.User �� ClaimsPrincipal ���͵�ʵ�����������˵�ǰ���������֤���û���
      //          context.User.Claims ��ʾ��ǰ���������֤���û������е��������ϡ�
      //          �����ʹ�� Claims ���������ʺͲ�����Щ�������������ض����͵������Ƿ���ڡ���ȡ������ֵ�ȡ�
      //     */
      //    {
      //        /* HasClaim ��������һ��ν�ʣ�Predicate����Ϊ����������ָ����Ҫ���������������
      //         * �����ν�� c => c.Type == ClaimTypes.Role ��ʾҪ�����������ͣ�Type��Ϊ "Role"��
      //         * ����û����� ����һ������ Ϊ "Role" ������������������� true��
      //         */
      //        bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role)
      //        /* First �������ڻ�ȡ��һ��������Ȼ��ͨ�� Value ���Ի�ȡ��������ֵ���бȽϡ�
      //           �����һ����ɫ������ֵ���� "admin"������������� true��*/
      //           && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "admin"
      //           && context.User.Claims.Any(c => c.Type == ClaimTypes.Name);
      //        return bResult;
      //        /*  ���� c ��������ͬ��λ��ʹ�ã����京������ͬ�ġ�
      //            �û�������Claim������ʱ������������ LINQ ��ѯ�н�������ƥ���ֵ��ȡ��*/
      //    });

      //});
  }) //������Ȩ
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  /* ����������Ϊʹ�� JWT Bearer �����֤������JwtBearerDefaults.AuthenticationScheme����
   * ��������ʹ�� .AddJwtBearer ���������� JWT Bearer �����֤��ѡ����߼���*/
    .AddJwtBearer(options =>  //���������õļ�Ȩ���߼�
    {
        //��.AddJwtBearer �����У�����ͨ�� options.TokenValidationParameters ���������� JWT �����֤�Ĳ�����
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //�����JWT��JSON Web Token����Ϳ�����Token������

            //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��

            //�������Ϊtrue�������֤ JWT �� Issuer �Ƿ��� ValidIssuer���� ��ָ����ֵƥ�䡣
            ValidateIssuer = true,//ָ���Ƿ���֤ JWT �е� Issuer��ǩ���ߣ���
            //�������Ϊtrue�������֤ JWT �� Audience �Ƿ��� ValidAudience���� ��ָ����ֵƥ�䡣
            ValidateAudience = true,//ָ���Ƿ���֤ JWT �е� Audience�����ڣ���

            ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
            //�������Ϊtrue��ϵͳ���Զ���Ⲣ��֤����Ҫ��IssuerSigningKey�����������
            ValidateIssuerSigningKey = true,//ָ���Ƿ���֤ JWT �еļ�����Կ��
            /*���û��ָ��IssuerSigningKey����ʹValidateIssuerSigningKey����Ϊtrue��
             *��֤��Ȼ��ʧ�ܣ�����ValidateIssuerSigningKey���Խ���Ч��*/

            ValidAudience = tokenOptions.Audience,//ָ����Ч�� Audience ֵ�������� JWT �е� Audience ���бȽ���֤��
            ValidIssuer = tokenOptions.Issuer,//ָ����Ч�� Issuer ֵ�������� JWT �е� Issuer ���бȽ���֤��

            // ָ��������֤ JWT �ĶԳƼ�����Կ��
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),

            //AudienceValidator������LifetimeValidator�����������Զ�����֤ JSON Web Token(JWT) �еĲ�����ί�з�����

            //�����ί�з����У�����Ա�д�Լ�����֤�߼�����һ����֤ JWT �� Audience��
            AudienceValidator = (m, n, z) =>
            //ί�еĲ���(m, n, z)�ֱ��ʾ JWT �е� Audience ֵ����ǰ�İ�ȫ����(token)���Լ���֤������
            {

                Console.WriteLine($"###### {m.FirstOrDefault()}");
                Console.WriteLine($"###### { m != null && m.FirstOrDefault().Equals("http://localhost:5200")}");
                //�������д�Լ��������֤�߼�
                return m != null && m.FirstOrDefault().Equals("http://localhost:5200");
                //Ҫ�� Audience ֵ��Ϊnull�����ҵ�һ��Ԫ����Ԥ�ڵ� Audience ֵ���ʱ������true��ʾ��֤ͨ��

                //return true;
            },
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            /*  notBefore ��ʾ JWT ����Чʱ�䣨���ڴ�ʱ��֮ǰ JWT ����Ч�ģ���
                expires ��ʾ JWT �Ĺ���ʱ�䣨���ڴ�ʱ��֮�� JWT ����Ч�ģ���
                securityToken ��ʾҪ��֤�İ�ȫ����(token)
                validationParameters ����֤������*/
            {
                // Ҫ�� ��ǰʱ��DateTime.Now ��JWT�Ĺ���ʱ��expires ֮ǰ ����ͨ����֤
                Console.WriteLine($"###### ��ǰʱ��{DateTime.Now} ### ����ʱ��{expires} #########"); ;
                Console.WriteLine($"###### {expires >= DateTime.Now}");

                return expires >= DateTime.Now; // DateTime�������ݿ��ԱȽϴ�С
                ////&& validationParameters

                //return true;

            }//�Զ���У�����
        };
    });
#endregion

WebApplication app = builder.Build(); //ʹ�ù�����builder������Ӧ�ó���ʵ��app��

// Configure the HTTP request pipeline. // ����HTTP����ܵ���
// ͨ��if(app.Environment.IsDevelopment())�жϵ�ǰ�����Ƿ�Ϊ����������
if (app.Environment.IsDevelopment())
{
    //����Swagger�м��������
    //app.UseSwaggerExt();  // Ϊ�˽�ʡProgram�еĿռ䣬������߼���д��SwaggerExtension��ȥ��
}

app.UseSwaggerExt();//��IIS����CoreWebApiʱҲ����Swagger

app.UseHttpsRedirection(); //���ڽ�HTTP�����ض���HTTPS����ǿ��ȫ��

app.UseAuthentication();//��Ȩ    ----��������ʱ�򣬰������д���token/Session/Cookies��������ȡ���û���Ϣ

// �������������֤����Ȩ���ܣ�������ɫ��Ȩ�Ͳ�����Ȩ
app.UseAuthorization(); //��Ȩ    --- �Ѿ��õ����û���Ϣ���Ϳ���ͨ���û���Ϣ���ж���ǰ�û��Ƿ���Է��ʵ�ǰ��Դ

app.MapControllers(); //��������·�ɵ���Ӧ�Ķ���������

app.Run(); //����Ӧ�ó��򣬼����ʹ������HTTP����

//��Microsoft.AspNetCore.Mvc.Versioning������ʵ�ְ汾���ƣ������ѱ�����

//����̨��ӡ��
//info: Microsoft.Hosting.Lifetime[14]
//      Now listening on: https://localhost:7012
//info: Microsoft.Hosting.Lifetime[14]
//      Now listening on: http://localhost:5199
//info: Microsoft.Hosting.Lifetime[0]
//      Application started. Press Ctrl+C to shut down.
//info: Microsoft.Hosting.Lifetime[0]
//      Hosting environment: Development
//info: Microsoft.Hosting.Lifetime[0]
//      Content root path: D:\C#\NET6-WebApi\NET6.DEMO\NET6.DEMO.WebApi\

//������Щ��־��Ϣ������Ӧ�ó�����й��������ڣ�Microsoft.Hosting.Lifetime��ģ�顣�����ṩ���й�Ӧ�ó��������״̬�����õ���Ϣ��

/*
Now listening on: https://localhost:7012����ʾӦ�ó�����������λ�ڱ���������localhost���Ķ˿�7012�ϵ�HTTPS����
��Ӧ�ó����ѳɹ����������ڽ��ܰ�ȫ���ӵ�����

�������˿���Ӧ�ó����ڲ�ͬЭ�飨HTTP��HTTPS���¼����Ķ˿ڣ����ڽ��ܿͻ��˵�����

Now listening on: http://localhost:5199����ʾӦ�ó�����������λ�ڱ���������localhost���Ķ˿�5199�ϵ�HTTP����
��Ӧ�ó����ѳɹ����������ڽ��ܷǰ�ȫ���ӵ�������������ڿ��������У�Ӧ�ó���ͬʱ֧��HTTP��HTTPS��

Application started. Press Ctrl+C to shut down.����ʾӦ�ó����������ɹ�������һ����ʾ����֪�����԰���Ctrl+C��ϼ����ر�Ӧ�ó���

Hosting environment: Development����ʾ��ǰ���йܻ���Ϊ��������������һ��ָʾ����֪��Ӧ�ó���ǰ�����Կ������������ú��������С�

Content root path: D:\C#\NET6-WebApi\NET6.DEMO\NET6.DEMO.WebApi\����ʾӦ�ó�������ݸ�·������Ӧ�ó������ڵĸ��ļ��е�·����
����������У�Ӧ�ó�����ļ��е�·����D:\C#\NET6-WebApi\NET6.DEMO\NET6.DEMO.WebApi\�� 

��Щ��־��Ϣ�ṩ��Ӧ�ó���Ĺؼ�����ʱ��Ϣ�����������Ķ˿ڡ�����״̬���йܻ��������ݸ�·����
���Ƕ��ڵ��Ժͼ���Ӧ�ó�������зǳ����ã������԰������˽�Ӧ�ó�������úͲ��������
 */
