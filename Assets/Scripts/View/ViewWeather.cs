using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class ViewWeather : MonoBehaviour
{
    [SerializeField] private RawImage img_Icon;
    [SerializeField] private TMP_Text txt_Info;

    public void DisplayWeather(string weatherInfo)
    {
        txt_Info.SetText(weatherInfo);
    }
    public void ClearWeather()
    {
        txt_Info.SetText(string.Empty);
    }

    public void SetWeatherIcon(Texture icon)
    {
        img_Icon.texture = icon;
    }
}