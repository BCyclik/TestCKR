using System.Collections.Generic;
using System.Threading.Tasks;

public class QueueManager
{
    private Queue<Task> _requestQueue = new();

    public void Enqueue(Task request)
    {
        _requestQueue.Enqueue(request);

        if (_requestQueue.Count == 1) ProcessQueueAsync();
    }

    public async void ProcessQueueAsync()
    {
        while (_requestQueue.Count > 0)
        {
            var currentRequest = _requestQueue.Dequeue();
            await currentRequest;
        }
    }
}