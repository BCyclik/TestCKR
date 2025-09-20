using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ViewClicker : MonoBehaviour
{
    [SerializeField] private ViewInfo info_Coins;
    [SerializeField] private ViewInfo info_Energy;
    [Space]
    [SerializeField] private Button btn_Clicker;

    public event Action OnClicked;

    private void Awake()
    {
        btn_Clicker.onClick.AddListener(Click);
    }
    public void SetEnergy(int value)
    {
        info_Energy.SetInfo(value);
    }
    [Space]
    [SerializeField] private ViewNumber prefabViewNumber;
    public void SetCurrency(int value)
    {
        info_Coins.SetInfo(value);
    }
    public void Click()
    {
        OnClicked?.Invoke();
    }
    public async void AutoClick(PointerEventData pointerEventData) // выглядит костыльно (
    {
        btn_Clicker.OnPointerDown(pointerEventData);
        await Task.Delay(100);
        btn_Clicker.OnPointerUp(pointerEventData);
        Click();
    }
    public void CreateNumber(int currency)
    {
        var obj = Instantiate(prefabViewNumber, btn_Clicker.transform, false);
        obj.transform.position = btn_Clicker.transform.position + new Vector3(UnityEngine.Random.Range(-200, 200), UnityEngine.Random.Range(-150, 100));
        string str = currency > 0 ? "+" + currency : currency.ToString();
        obj.txt.SetText(str);
    }
}