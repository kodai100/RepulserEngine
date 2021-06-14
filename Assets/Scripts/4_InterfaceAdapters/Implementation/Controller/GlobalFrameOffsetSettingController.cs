using System;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class GlobalFrameOffsetSettingController : IGlobalFrameOffsetSettingController
    {
        public IObservable<GlobalFrameOffset> OnDataSavedAsObservable => useCase.OnDataSavedAsObservable;
        public IObservable<GlobalFrameOffset> OnDataUpdatedAsObservable => useCase.OnDataUpdatedAsObservable;

        IGlobalFrameOffsetSettingUseCase useCase;

        public GlobalFrameOffsetSettingController(IGlobalFrameOffsetSettingUseCase settingUseCase)
        {
            this.useCase = settingUseCase;
        }

        public void Save()
        {
            useCase.Save();
        }

        public void Update(GlobalFrameOffset frameOffset)
        {
            useCase.Update(frameOffset);
        }

        public GlobalFrameOffset Load()
        {
            return useCase.Load();
        }
    }

    public class GlobalFrameOffsetSettingControllerInstaller : Installer<GlobalFrameOffsetSettingControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GlobalFrameOffsetSettingController>().AsSingle();
        }
    }
}