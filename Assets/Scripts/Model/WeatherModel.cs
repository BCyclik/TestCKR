using UnityEngine;
using System;

public class WeatherModel
{
    public event Action<string> OnWeatherChanged;
    public event Action<Texture> OnIconChanged;

    private Texture icon;
    private string info;
    public void SetIcon(Texture icon)
    {
        this.icon = icon;
        OnIconChanged?.Invoke(icon);
    }

    public void SetInfo(string info)
    {
        this.info = info;
        OnWeatherChanged?.Invoke(info);
    }
}