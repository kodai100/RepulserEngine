using System;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class ObsWebsocketCommunicationUseCase : IObsWebsocketCommunicationUseCase
    {
        public IObservable<string> OnConnected => repository.OnConnected;
        public IObservable<string> OnDisconnected => repository.OnDisconnected;
        public IObservable<string> OnSceneChanged => repository.OnSceneChanged;

        IObsWebsocketCommunicationRepository repository;

        public ObsWebsocketCommunicationUseCase(IObsWebsocketCommunicationRepository repository)
        {
            this.repository = repository;
        }

        public bool Connect(string serverUrl, string password)
        {
            return repository.Connect(serverUrl, password);
        }

        public void Disconnect()
        {
            repository.Disconnect();
        }

        public void SetScene(string sceneName)
        {
            repository.SetScene(sceneName);
        }

        public void RestartMediaSource(string mediaSourceName)
        {
            repository.RestartMediaSource(mediaSourceName);
        }
    }

    public class ObsWebsocketCommunicationUseCaseInstaller : Installer<ObsWebsocketCommunicationUseCaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketCommunicationUseCase>().AsSingle();
        }
    }
}