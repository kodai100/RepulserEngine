using ProjectBlue.RepulserEngine.Data.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class ObsWebsocketSettingRepository : IObsWebsocketSettingRepository
    {
        IObsWebsocketSettingDataStore dataStore;

        public ObsWebsocketSettingRepository(IObsWebsocketSettingDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public ObsWebsocketSettingDataModel Load()
        {
            return dataStore.Load();
        }

        public void Save(ObsWebsocketSettingDataModel data)
        {
            dataStore.Save(data);
        }
    }

    public class ObsWebsocketSettingRepositoryInstaller : Installer<ObsWebsocketSettingRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketSettingRepository>().AsSingle();
        }
    }
}