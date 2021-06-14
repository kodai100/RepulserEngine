using System;
using ProjectBlue.RepulserEngine.Data.DataStore;
using ProjectBlue.RepulserEngine.Domain.Entity;
using Zenject;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class GlobalFrameOffsetSettingRepository : IGlobalFrameOffsetSettingRepository
    {
        public IObservable<GlobalFrameOffset> OnDataSavedAsObservable => dataStore.OnDataSavedAsObservable;

        IGlobalFrameOffsetSettingDataStore dataStore;

        public GlobalFrameOffsetSettingRepository(IGlobalFrameOffsetSettingDataStore settingDataStore)
        {
            this.dataStore = settingDataStore;
        }

        public void Save(GlobalFrameOffset frameOffset)
        {
            dataStore.Save(frameOffset);
        }

        public GlobalFrameOffset Load()
        {
            return dataStore.Load();
        }
    }

    public class GlobalFrameOffsetSettingRepositoryInstaller : Installer<GlobalFrameOffsetSettingRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GlobalFrameOffsetSettingRepository>().AsSingle();
        }
    }
}