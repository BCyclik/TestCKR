using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
public class ControllerClicker : MonoBehaviour
{
    [Inject] private Parametrs parametrs;
    [Inject]
    public void Construct(Parametrs parametrs)
    {
        this.parametrs = parametrs;
    }
    private ClickerModel clickerModel = new();

    [SerializeField] private ViewClicker viewClicker;

    private void Awake()
    {
        timer_delay_auto_click = TimeDelayAutoClick;
        timer_recharge_energy = TimeRechargeEnergy;

        viewClicker.SetCurrency(clickerModel.Currency);
        viewClicker.SetEnergy(clickerModel.Energy);

        clickerModel.OnCurrencyChanged += viewClicker.SetCurrency;
        clickerModel.OnEnergyChanged += viewClicker.SetEnergy;

        viewClicker.OnClicked += Click;
    }
    private void Start()
    {
        clickerModel.MaxEnergy = parametrs.MaxEnergy;
        clickerModel.Energy = parametrs.MaxEnergy;
    }
    private int Energy { get => clickerModel.Energy; set => clickerModel.Energy = value; }
    private AudioSource audioSource => GetComponent<AudioSource>();
    public void Click()
    {
        if (Energy <= 0) return;

        viewClicker.CreateNumber(parametrs.Currency);
        clickerModel.Currency += parametrs.Currency; // ??
        audioSource.PlayOneShot(audioSource.clip);
        Energy -= parametrs.Energy;

    }
    //[Space]
    [SerializeField] private float TimeDelayAutoClick => parametrs.DelayAutoClick;
    private float timer_delay_auto_click = 0;
    [SerializeField] private float TimeRechargeEnergy => parametrs.DelayEnergyRecharge;
    private float timer_recharge_energy = 0;

    private PointerEventData pointerEventData = new(EventSystem.current) { button = PointerEventData.InputButton.Left };

    private void Update()
    {
        if (timer_recharge_energy > 0)
        {
            timer_recharge_energy -= Time.deltaTime;
        }
        else
        {
            timer_recharge_energy = TimeRechargeEnergy;
            clickerModel.RechargeEnergy(parametrs.EnergyGain);
        }

        if (timer_delay_auto_click > 0)
        {
            timer_delay_auto_click -= Time.deltaTime;
        } 
        else
        {
            timer_delay_auto_click = TimeDelayAutoClick;
            viewClicker.AutoClick(pointerEventData);
        }
    }
}