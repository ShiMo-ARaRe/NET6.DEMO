// 为WeatherForecastController（天气控制器类）提供数据模型

namespace NET6.DEMO.WebApi
{
    public class WeatherForecast
    {
        //这是一个公共的属性，用于表示天气预报的日期。
        //它具有一个 DateTime 类型的 Date 属性，并且可以读取和设置。
        public DateTime Date { get; set; }

        //这是一个公共的属性，用于表示摄氏温度。
        //它具有一个 int 类型的 TemperatureC 属性，并且可以读取和设置。
        public int TemperatureC { get; set; }

        //这是一个公共的只读属性，用于表示华氏温度。
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        //这是一个公共的属性，用于表示天气预报的摘要。
        public string? Summary { get; set; }
    }
}
