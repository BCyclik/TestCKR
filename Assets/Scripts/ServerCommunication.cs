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

        var asyncOperation = webRequest.SendWebRequest(); // ������

        while (!asyncOperation.isDone)
        {
            // �������� �� ������
            if (cancellationToken.IsCancellationRequested)
            {
                webRequest.Abort();
                return;
            }
            await Task.Yield();
        }

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"������: {webRequest.error}");
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

        // ��� ������ �������
        var asyncOperation = webRequest.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            if (token.IsCancellationRequested)
            {
                webRequest.Abort();  // �������� ������
                throw new OperationCanceledException();
            }
            await Task.Yield(); // ��������� ������ ��������� ����������� ���� ���� ����������
        }

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(webRequest.error);
        }

        Texture texture = DownloadHandlerTexture.GetContent(webRequest);
        // ��������� �������� � ����
        string fileName = GetFileNameWithPngExtension(url);
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // ��������� ������ � ������� PNG
        byte[] textureData = ((Texture2D)texture).EncodeToPNG();
        
        File.WriteAllBytes(filePath, textureData);
        Debug.Log($"Image saved to: {fileName}");

        return texture;
    }

    public string GetFileNameWithPngExtension(string path)
    {
        path = path.Replace("\\", "/");
        // ������� ������ ���������� ��������� '\'
        int lastSlashIndex = path.LastIndexOf("/");

        // ���������, ��� ����������� ������
        if (lastSlashIndex != -1)
        {
            // �������� ��������� ����� ���������� '\'
            string lastPart = path.Substring(lastSlashIndex + 1);

            // ������� ������ ������� '?'
            int queryIndex = lastPart.IndexOf('?');

            // ���� '?' ������, �������� ��� ����� ����
            if (queryIndex != -1)
            {
                lastPart = lastPart.Substring(0, queryIndex);
            }

            // ������� ���������� ����� (���� ��� ����)
            int dotIndex = lastPart.LastIndexOf('.');
            if (dotIndex != -1)
            {
                lastPart = lastPart.Substring(0, dotIndex);
            }

            // ����������� ���������� .png
            lastPart += ".png";

            //Debug.Log($"Image saved to: {lastPart}");

            return lastPart; // ���������� �������� ����� � �����������
        }

        return string.Empty; // ���� ��� �����������, ���������� ������ ������
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