//����Ĵ�������������ASP.NET CoreӦ�ó����HTTP������ܵ��ͷ��������� // ��������

//����һ��Ӧ�ó��򹹽���builder���ù������������ú͹���Ӧ�ó���
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//��������������ӵ�����ע�������С���������ʹӦ�ó����ܹ�ʹ��ASP.NET Core MVC������������ӦHTTP����
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//�ֱ����ApiExplorer��SwaggerGen���������С���Щ������Swagger/OpenAPI��أ�
//�������ɺ��ṩAPI�ĵ�����ApiExplorer��������API��Ԫ���ݣ���SwaggerGen��������Swagger�淶���ĵ���


var app = builder.Build(); //ʹ�ù�����builder������Ӧ�ó���ʵ��app��

// Configure the HTTP request pipeline.
// ͨ��if(app.Environment.IsDevelopment())�жϵ�ǰ�����Ƿ�Ϊ����������
if (app.Environment.IsDevelopment())
{
    // ����ǿ����������ͻ�����Swagger��Swagger UI��
    // ʹ��app.UseSwagger()��app.UseSwaggerUI()��Swagger�м����ӵ�������ܵ��У�
    // �Ա���������в鿴�ͽ���ʽ����API�ĵ���
    app.UseSwagger();
    app.UseSwaggerUI();
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
