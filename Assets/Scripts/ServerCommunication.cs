using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Threading;
using UnityEngine;
using System.IO;
using System;

public class ServerCommunication
{
    public async Task SendRequest(string url, CancellationTokenSource cancellationToken, Action<string, CancellationTokenSource> action)
    {
        Debug.Log($"SendRequest: {url}");

        var webRequest = UnityWebRequest.Get(url);
        //webRequest.SendWebRequest();
        //if (token.IsCancellationRequested) return null;

        var asyncOperation = webRequest.SendWebRequest(); // запрос

        while (!asyncOperation.isDone)
        {
            // Проверка на отмену
            if (cancellationToken.IsCancellationRequested)
            {
                webRequest.Abort();
                return;
            }
            await Task.Yield();
        }

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Ошибка: {webRequest.error}");
            return;
        }
        else
        {
            string result = webRequest.downloadHandler.text;
            action?.Invoke(result, cancellationToken);
            //return webRequest.downloadHandler.text;
        }
    }
    public async Task<Texture> DownloadImage(string url, CancellationTokenSource token)
    {
        Debug.Log($"DownloadImage: {url}");

        var webRequest = UnityWebRequestTexture.GetTexture(url);

        // Для отмены запроса
        var asyncOperation = webRequest.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            if (token.IsCancellationRequested)
            {
                webRequest.Abort();  // Отменяем запрос
                throw new OperationCanceledException();
            }
            await Task.Yield(); // Позволяем другим процессам выполняться пока ждем завершения
        }

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(webRequest.error);
        }

        Texture texture = DownloadHandlerTexture.GetContent(webRequest);
        // Сохраняем текстуру в файл
        string fileName = GetFileNameWithPngExtension(url);
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Получение данных в формате PNG
        byte[] textureData = ((Texture2D)texture).EncodeToPNG();
        
        File.WriteAllBytes(filePath, textureData);
        Debug.Log($"Image saved to: {fileName}");

        return texture;
    }

    public string GetFileNameWithPngExtension(string path)
    {
        path = path.Replace("\\", "/");
        // Находим индекс последнего вхождения '\'
        int lastSlashIndex = path.LastIndexOf("/");

        // Проверяем, что разделитель найден
        if (lastSlashIndex != -1)
        {
            // Получаем подстроку после последнего '\'
            string lastPart = path.Substring(lastSlashIndex + 1);

            // Находим индекс символа '?'
            int queryIndex = lastPart.IndexOf('?');

            // Если '?' найден, обрезаем все после него
            if (queryIndex != -1)
            {
                lastPart = lastPart.Substring(0, queryIndex);
            }

            // Удаляем расширение файла (если оно есть)
            int dotIndex = lastPart.LastIndexOf('.');
            if (dotIndex != -1)
            {
                lastPart = lastPart.Substring(0, dotIndex);
            }

            // Приписываем расширение .png
            lastPart += ".png";

            //Debug.Log($"Image saved to: {lastPart}");

            return lastPart; // Возвращаем название файла с расширением
        }

        return string.Empty; // Если нет разделителя, возвращаем пустую строку
    }

    public async Task LoadImage(string url, CancellationTokenSource token, Action<Texture, CancellationTokenSource> action)
    {
        string fileName = GetFileNameWithPngExtension(url);
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("FileName:" + fileName + "; FilePath" + filePath);
        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            action?.Invoke(texture, token);
        } 
        else
        {
            var texture = await DownloadImage(url, token);
            action?.Invoke(texture, token);
        }
    }
}