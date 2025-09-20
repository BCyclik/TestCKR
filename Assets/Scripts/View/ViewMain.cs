using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ViewMain : MonoBehaviour
{
    [SerializeField] private ViewClicker viewClicker;
    [SerializeField] private ViewWeather viewWeather;
    [SerializeField] private ViewListDogs viewListDogs;
    [Space]
    [SerializeField] private Button btn_Clicker;
    [SerializeField] private Button btn_Weather;
    [SerializeField] private Button btn_ListDogs;
    private void Awake()
    {
        btn_Clicker.onClick.AddListener(() => ChangeTab(0));
        btn_Weather.onClick.AddListener(() => ChangeTab(1));
        btn_ListDogs.onClick.AddListener(() => ChangeTab(2));
    }
    public void ChangeTab(int index)
    {
        viewClicker.gameObject.SetActive(index == 0);
        viewWeather.gameObject.SetActive(index == 1);
        viewListDogs.gameObject.SetActive(index == 2);
    } 
}