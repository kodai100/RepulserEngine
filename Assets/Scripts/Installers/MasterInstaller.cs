using System;
using ProjectBlue.RepulserEngine;
using Zenject;

public class MasterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<RepulserUseCase>().AsSingle().NonLazy();

    }
}