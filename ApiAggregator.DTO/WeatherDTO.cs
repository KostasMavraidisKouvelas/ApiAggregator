namespace ApiAggregator.DTO
{

    public class WeatherResponseDto
    {
    public string Cod { get; set; }
    public double Message { get; set; }
    public int Cnt { get; set; }
    public List<WeatherDto> List { get; set; }
    }
public class WeatherDto
    {
        public long Dt { get; set; }
        public MainDto Main { get; set; }
        public List<WeatherInfoDTO> Weather { get; set; }
        public CloudsDTO Clouds { get; set; }
        public WindDTO Wind { get; set; }
        public int Visibility { get; set; }
        public double Pop { get; set; }
        public RainDTO Rain { get; set; }
        public SysDTO Sys { get; set; }
        public string DtTxt { get; set; }
    }

    public class MainDto
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }
        public int Humidity { get; set; }
        public double TempKf { get; set; }
    }

    public class WeatherInfoDTO
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class CloudsDTO
    {
        public int All { get; set; }
    }

    public class WindDTO
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }
    }

    public class RainDTO
    {
        public double ThreeHour { get; set; }
    }

    public class SysDTO
    {
        public string Pod { get; set; }
    }
}
