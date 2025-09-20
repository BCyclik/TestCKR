using Zenject.SpaceFighter;
using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("InstallBindings called!");

        Container.Bind<ServerCommunication>().AsSingle();
        Container.Bind<QueueManager>().AsSingle();
    }
}