using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;
using System;

public class ControllerWeather : MonoBehaviour
{
    [Inject] private ServerCommunication serverCommunication;
    [Inject] private QueueManager queueManager;

    private WeatherModel weatherModel = new();
    [SerializeField] private ViewWeather viewWeather;
    private void OnEnable()
    {
        SendRequest_UpdateWeather();
    }
    private void Awake()
    {
        weatherModel.OnWeatherChanged += viewWeather.DisplayWeather;
        weatherModel.OnIconChanged += viewWeather.SetWeatherIcon;
    }
    [Space]
    [SerializeField] private float TimeDelay = 5f;
    private float timer_delay = 0;
    private void Update()
    {
        if (timer_delay > 0)
        {
            timer_delay -= Time.deltaTime;
        } 
        else
        {
            SendRequest_UpdateWeather();
        }
    }
    private List<CancellationTokenSource> list_requestes = new(); // на случай, если будет > 1 запроса с контроллера
    private void SendRequest_UpdateWeather()
    {
        string url = "https://api.weather.gov/gridpoints/TOP/32,81/forecast"; // вынести в ScriptableObj
        var token = new CancellationTokenSource();

        queueManager.Enqueue(serverCommunication.SendRequest(url, token, UpdateWeather)); // ???
        list_requestes.Add(token);

        timer_delay = TimeDelay;
    }
    private void UpdateWeather(string result, CancellationTokenSource token)
    {
        Debug.Log($"UpdateWeather({result})");
        var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(result).properties;
        if (weatherData == null) return;
        DateTime today = DateTime.Now;

        for (int i = 0; i < weatherData.periods.Count; i++)
        {
            var period = weatherData.periods[i];
            if (!DateTime.TryParse(period.startTime, out DateTime startTime)) continue;
            if (!DateTime.TryParse(period.endTime, out DateTime endTime)) continue;
            Debug.Log($"StartTime: {startTime}, EndTime: {endTime}");

            if (startTime <= today && today <= endTime)
            {
                Debug.Log($"Сегодня: {period.name}, Температура: {period.temperature} {period.temperatureUnit}");

                string str = $"Сегодня {period.temperature} {period.temperatureUnit}";
                weatherModel.SetInfo(str); // меняем модель

                var newToken = new CancellationTokenSource();
                queueManager.Enqueue(serverCommunication.LoadImage(period.icon, newToken, LoadIcon)); // ???
                list_requestes.Add(newToken);
                //string url_image = period.icon;
                //LoadIcon(url_image, null);
                break;
            }
        }

        list_requestes.Remove(token);
    }
    private void LoadIcon(Texture texture, CancellationTokenSource token) 
    {
        weatherModel.SetIcon(texture);

        list_requestes.Remove(token);
    }
    private void OnDisable()
    {
        for (int i = 0; i < list_requestes.Count; i++)
        {
            list_requestes[i].Cancel();
        }
        list_requestes.Clear();
    }
}