using UnityEngine;
using Zenject;

public class AppController : MonoBehaviour
{
    [SerializeField] private ViewMain viewMain;
    private void Start()
    {
        viewMain.ChangeTab(0);
    }
}