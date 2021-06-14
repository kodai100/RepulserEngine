using System;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class GlobalFrameOffsetSettingUseCase : IGlobalFrameOffsetSettingUseCase, IDisposable
    {
        public IObservable<GlobalFrameOffset> OnDataSavedAsObservable => repository.OnDataSavedAsObservable;
        public IObservable<GlobalFrameOffset> OnDataUpdatedAsObservable => onDataUpdated;
        private Subject<GlobalFrameOffset> onDataUpdated = new Subject<GlobalFrameOffset>();

        private GlobalFrameOffset valueCache;

        IGlobalFrameOffsetSettingRepository repository;

        public GlobalFrameOffsetSettingUseCase(IGlobalFrameOffsetSettingRepository settingRepository)
        {
            this.repository = settingRepository;
        }

        public void Save()
        {
            repository.Save(valueCache);
        }

        public void Update(GlobalFrameOffset frameOffset)
        {
            valueCache = frameOffset;
        }

        public GlobalFrameOffset Load()
        {
            valueCache = repository.Load();
            return valueCache;
        }

        public void Dispose()
        {
        }
    }

    public class GlobalFrameOffsetSettingUseCaseInstaller : Installer<GlobalFrameOffsetSettingUseCaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GlobalFrameOffsetSettingUseCase>().AsSingle();
        }
    }
}