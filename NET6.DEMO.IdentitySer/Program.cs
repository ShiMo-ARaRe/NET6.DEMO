using NET6.Demo.IdentitySer.Utility;

//���ܽ���token https://jwt.io/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*  ����-->����
 builder.Services: builder ��һ��WebHostBuilder��HostBuilderʵ�������ڹ���������Ӧ�ó����������
Services������WebHostBuilder��HostBuilder��һ����Ա�����������Ƿ���Ӧ�ó��������ע��������

Configure<JWTTokenOptions>: ����һ�����ͷ��������������ض����ͣ�JWTTokenOptions���ķ���
����������£�������������JWTTokenOptions���͵ķ���

uilder.Configuration: ����һ����ʾӦ�ó������õĶ������ṩ�˷���Ӧ�ó�������ֵ�ķ�����

GetSection("JWTTokenOptions"): ����һ����Ӧ�ó������ã������ļ���appsettings.json���л�ȡ�ض����ò��ֵķ�����
������������ڻ�ȡ��ΪJWTTokenOptions�����ò��֡�
����һ��������ֵ�󶨵�ָ�����͵ķ�����ͨ���������ͣ�JWTTokenOptions�������ò��֣�
��������ֵ��JWTTokenOptions������Խ���ƥ��Ͱ󶨡�

�ܽ���ǣ����д����������ʹ������ע������������һ����ΪJWTTokenOptions��ѡ���࣬
��ѡ��������Խ���Ӧ�ó������õ�JWTTokenOptions�����л�ȡ��Ӧ������ֵ��
 */
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));// ����-->����
/* ����ע��
��CustomHSJWTService��ע��ΪICustomJWTService�ӿڵ�ʵ���࣬��������ӵ�����ע�������С�
���������ǿ�����Ӧ�ó������ط�ͨ������ע���ȡICustomJWTService�ӿڵ�ʵ������ʹ��CustomHSJWTService��Ĺ��ܡ�
 */
builder.Services.AddTransient<ICustomJWTService, CustomHSJWTService>(); // �Գƿ������
//builder.Services.AddTransient<ICustomJWTService, CustomRSSJWTervice>(); // �ǶԳƿ������

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
