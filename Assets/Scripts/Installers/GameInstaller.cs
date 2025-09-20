using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

[CreateAssetMenu(fileName = "GameInstaller ", menuName = "Installers/GameInstaller ")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    public Parametrs parametrs;

    public override void InstallBindings()
    {
        Container.Bind<Parametrs>().FromInstance(parametrs).AsSingle();

    }
}