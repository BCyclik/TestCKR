using System;
using System.Collections.Generic;

[Serializable]
public class WeatherResponse
{
    public Properties properties { get; set; }
}

[Serializable]
public class Properties
{
    public string units { get; set; }
    public string forecastGenerator { get; set; }
    public string generatedAt { get; set; }
    public string updateTime { get; set; }
    public string validTimes { get; set; }
    public Elevation elevation { get; set; }
    public List<Period> periods { get; set; }
}

[Serializable]
public class Elevation
{
    public string unitCode { get; set; }
    public double value { get; set; }
}

[Serializable]
public class Period
{
    public int number { get; set; }
    public string name { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    public bool isDaytime { get; set; }
    public int temperature { get; set; }
    public string temperatureUnit { get; set; }
    public string temperatureTrend { get; set; }
    public ProbabilityOfPrecipitation probabilityOfPrecipitation { get; set; }
    public string windSpeed { get; set; }
    public string windDirection { get; set; }
    public string icon { get; set; }
    public string shortForecast { get; set; }
    public string detailedForecast { get; set; }
}

[Serializable]
public class ProbabilityOfPrecipitation
{
    public string unitCode { get; set; }
    public int value { get; set; }
}
