// ΪWeatherForecastController�������������ࣩ�ṩ����ģ��

namespace NET6.DEMO.WebApi
{
    public class WeatherForecast
    {
        //����һ�����������ԣ����ڱ�ʾ����Ԥ�������ڡ�
        //������һ�� DateTime ���͵� Date ���ԣ����ҿ��Զ�ȡ�����á�
        public DateTime Date { get; set; }

        //����һ�����������ԣ����ڱ�ʾ�����¶ȡ�
        //������һ�� int ���͵� TemperatureC ���ԣ����ҿ��Զ�ȡ�����á�
        public int TemperatureC { get; set; }

        //����һ��������ֻ�����ԣ����ڱ�ʾ�����¶ȡ�
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        //����һ�����������ԣ����ڱ�ʾ����Ԥ����ժҪ��
        public string? Summary { get; set; }
    }
}
