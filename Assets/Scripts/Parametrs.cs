using UnityEngine;

[CreateAssetMenu(fileName = "Parametrs", menuName = "Data/Parametrs")]
public class Parametrs : ScriptableObject
{
    [Header("Начисление валюты за клик")]
    public int Currency = 1;
    [Header("Отнимает энергии за клик")]
    public int Energy = 1;
    [Header("Максимальное кол-во энергии")] 
    public int MaxEnergy = 1000;
    [Header("Задержка автоклика")]
    public float DelayAutoClick = 3;
    //public int CurrencyAutoClick = 1;
    [Header("Задержка восстановление энергии")]
    public float DelayEnergyRecharge = 10;
    [Header("Кол-во восстановление энергии")]
    public int EnergyGain = 10;
}
