using System;
using UnityEngine;
using Zenject;

public class ClickerModel
{
    //[Inject] private Parametrs parametrs;
    //public void Construct(Parametrs parametrs)
    //{
    //   this.parametrs = parametrs;
    //}
    public event Action<int> OnCurrencyChanged;
    public event Action<int> OnEnergyChanged;

    private int currency = 0;
    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            OnCurrencyChanged?.Invoke(currency);
        }
    }
    //private int MaxEnergy => parametrs.MaxEnergy;
    private int energy = 0;
    public int Energy
    {
        get => energy;
        set
        {
            //energy = Mathf.Clamp(value, 0, MaxEnergy);
            energy = Mathf.Clamp(value, 0, MaxEnergy);
            OnEnergyChanged?.Invoke(energy);
        }
    }

    public int MaxEnergy { get; set; }

    //private int EnergyGain => parametrs.EnergyGain;

    public void RechargeEnergy(int ammount)
    {
        //if (Energy >= MaxEnergy) return; 
        Energy += ammount;
    }
}