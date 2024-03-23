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

//����һ��Ӧ�ó��򹹽���builder���ù������������ú͹���Ӧ�ó���
WebApplicationBuilder builder = WebApplication.CreateBuilder(args); // ��var�Զ��ƶ�����Ҳ��

// Add services to the container.// ��������ӵ������С�
//��������������ӵ�����ע�������С���������ʹӦ�ó����ܹ�ʹ��ASP.NET Core MVC������������ӦHTTP����
builder.Services.AddControllers(option =>
{
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

//����Swagger����������
builder.AddSwaggerGenExt(); // Ϊ�˽�ʡProgram�еĿռ䣬������߼���д��SwaggerExtension��ȥ�ˣ���������з�װ��˼��

WebApplication app = builder.Build(); //ʹ�ù�����builder������Ӧ�ó���ʵ��app��

// Configure the HTTP request pipeline. // ����HTTP����ܵ���
// ͨ��if(app.Environment.IsDevelopment())�жϵ�ǰ�����Ƿ�Ϊ����������
if (app.Environment.IsDevelopment())
{
    //����Swagger�м��������
    app.UseSwaggerExt();  // Ϊ�˽�ʡProgram�еĿռ䣬������߼���д��SwaggerExtension��ȥ��
}

app.UseHttpsRedirection(); //���ڽ�HTTP�����ض���HTTPS����ǿ��ȫ��

app.UseAuthorization(); //�������������֤����Ȩ���ܡ�

app.MapControllers(); //��������·�ɵ���Ӧ�Ķ���������

app.Run(); //����Ӧ�ó��򣬼����ʹ������HTTP����

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
