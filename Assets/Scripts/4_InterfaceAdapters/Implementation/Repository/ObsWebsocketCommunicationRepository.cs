using System;
using ProjectBlue.RepulserEngine.Data.DataStore;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class ObsWebsocketCommunicationRepository : IObsWebsocketCommunicationRepository
    {
        public IObservable<string> OnConnected => dataStore.OnConnected;
        public IObservable<string> OnDisconnected => dataStore.OnDisconnected;
        public IObservable<string> OnSceneChanged => dataStore.OnSceneChanged;

        IObsWebsocketCommunicationDataStore dataStore;

        public ObsWebsocketCommunicationRepository(IObsWebsocketCommunicationDataStore dataStore)
        {
            this.dataStore = dataStore;
        }


        public bool Connect(string serverUrl, string password)
        {
            return dataStore.Connect(serverUrl, password);
        }

        public void Disconnect()
        {
            dataStore.Disconnect();
        }

        public void SetScene(string sceneName)
        {
            dataStore.SetScene(sceneName);
        }

        public void RestartMediaSource(string mediaSourceName)
        {
            dataStore.RestartMediaSource(mediaSourceName);
        }
    }

    public class ObsWebsocketCommunicationRepositoryInstaller : Installer<ObsWebsocketCommunicationRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketCommunicationRepository>().AsSingle();
        }
    }
}