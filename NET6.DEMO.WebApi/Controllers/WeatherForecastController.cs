using Microsoft.AspNetCore.Mvc; //����һ�������ռ����ã���ʾ������ʹ����ASP.NET Core��MVC��ܡ�
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    //����һ�����Ա�ǣ�Ӧ���ڿ������ࡣ��ָʾ��������һ��Web API��������
    //���ҽ��Զ�Ӧ��һЩĬ�ϵ���Ϊ�����Զ�ģ����֤��HTTP��Ӧ���Զ��ƶϡ�
    [ApiController]

    //����һ�����Ա�ǣ�Ӧ���ڿ������ࡣ��ָʾ��������·��ģ�壬����[controller]�����滻Ϊ�����������ƣ�
    //��"WeatherForecast"������ζ�Ÿÿ�������·�ɽ���"/WeatherForecast"��ʼ��
    [Route("[controller]")]

    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V3))]

    //ControllerBase��ASP.NET Core�п������Ļ��ࡣ
    public class WeatherForecastController : ControllerBase
    {
        //����һ��˽�о�ֻ̬�����飬����һ������ժҪ�ַ�����
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //����һ��ֻ��˽���ֶΣ���ʾ����������־��¼������ʹ�÷���ILogger<T>������T��WeatherForecastController��
        //�Ա�������Ͱ�ȫ����־��¼��
        private readonly ILogger<WeatherForecastController> _logger;
        //ILogger<WeatherForecastController> ��һ�����ͽӿ����ͣ�������.NET CoreӦ�ó����н�����־��¼��

        //����һ���������Ĺ��캯��������һ��ILogger<WeatherForecastController>���͵Ĳ�����
        //���캯�����ڽ���־��¼��ע�뵽�������С�
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //����һ�����Ա�ǣ�Ӧ����Get()��������ָʾ�÷���������HTTP GET����
        //����·������Ϊ"GetWeatherForecast"��
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //Range����һ�� LINQ ��ѯ����������һ���������У��� 1 ��ʼ���� 5 ���������� 1 �� 5��
            //SelectӦ����ǰ�����ɵ��������С�����ÿ������ index��
            //��ִ��һ������������һ�� WeatherForecast ����
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                //�������� WeatherForecast ����� Date ���ԣ�����ǰ���ڼ��� index �졣
                Date = DateTime.Now.AddDays(index),

                //�������� WeatherForecast ����� TemperatureC ���ԣ�
                //����һ������ - 20 �� 55 ֮������������
                TemperatureC = Random.Shared.Next(-20, 55),

                //�������� WeatherForecast ����� Summary ���ԣ�
                //�� Summaries ���������ѡ��һ��ժҪ�ַ�����
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            //���ǽ� LINQ ��ѯ���ת��Ϊ WeatherForecast �����������ʽ������Ϊ���������շ��ؽ����
        }
        //IEnumerable<WeatherForecast> ��һ�����ͽӿ����ͣ�
        //��ʾһ�����Ե����ġ�������� WeatherForecast ����ļ��ϡ�

        //��ʹ�����飨WeatherForecast[]����ȣ�ʹ�� IEnumerable<WeatherForecast> ���ܻ���΢����һЩ���ܿ����ͱ��븴���ԡ�
        //������Ϊ IEnumerable<WeatherForecast> ��Ҫ���е������Ķ�̬���ã���������ʵ����ܸ��ߡ�
        //��ˣ�������Ҫ��ϸߵĳ����У����Կ���ʹ�����������������ļ������͡�
    }
}
