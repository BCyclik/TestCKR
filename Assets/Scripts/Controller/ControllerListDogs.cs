using Newtonsoft.Json;
using System.Threading;
using UnityEngine;
using Zenject;

public class ControllerListDogs : MonoBehaviour
{
    [Inject] private ServerCommunication serverCommunication;
    [Inject] private QueueManager queueManager;

    [SerializeField] private ViewListDogs viewListDogs;

    private void OnEnable()
    {
        SendRequest_GetListDogs();
    }
    private CancellationTokenSource current_token;
    private void SendRequest_GetListDogs()
    {
        string url = "https://dogapi.dog/api/v2/breeds"; // вынести в ScriptableObj
        current_token = new CancellationTokenSource();

        queueManager.Enqueue(serverCommunication.SendRequest(url, current_token, UploadListDogs)); // ???
    }
    private void UploadListDogs(string result, CancellationTokenSource token)
    {
        Debug.Log($"UploadListDogs({result})");
        var response = JsonConvert.DeserializeObject<DogListResponse>(result);
        if (response == null) return;

        var data = response.Data;
        for (int i = 0; i < data.Count; i++)
        {
            var item = viewListDogs.AddDog(data[i]);

            item.OnClick += LoadDogById;
        }

        viewListDogs.Loader.SetActive(false);
        current_token = null;
    }
    private void LoadDogById(ViewDog viewDog)
    {
        string url = "https://dogapi.dog/api/v2/breeds/" + viewDog.Id; // вынести в ScriptableObj
        viewListDogs.AllLoaders();
        viewDog.Loading(true);

        if (current_token != null) current_token.Cancel();
        current_token = new CancellationTokenSource();

        queueManager.Enqueue(serverCommunication.SendRequest(url, current_token, ShowPopup)); // ???
    }
    private void ShowPopup(string result, CancellationTokenSource token)
    {
        viewListDogs.AllLoaders();

        Debug.Log($"ShowPopup({result})");
        var response = JsonConvert.DeserializeObject<DogListSingleResponse>(result);
        if (response == null) return;

        viewListDogs.ShowPopup(response.Data);
        current_token = null;
    }
    private void OnDisable()
    {
        if (current_token != null) current_token.Cancel();
        viewListDogs.AllLoaders();
        current_token = null;
    }
}