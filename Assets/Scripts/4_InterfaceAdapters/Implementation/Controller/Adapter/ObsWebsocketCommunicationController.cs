using System;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class ObsWebsocketCommunicationController : IObsWebsocketCommunicationController
    {
        public IObservable<string> OnConnected => useCase.OnConnected;
        public IObservable<string> OnDisconnected => useCase.OnDisconnected;
        public IObservable<string> OnSceneChanged => useCase.OnSceneChanged;

        private IObsWebsocketCommunicationUseCase useCase;

        private IObsWebsocketSettingUseCase settingUseCase;

        public ObsWebsocketCommunicationController(IObsWebsocketCommunicationUseCase useCase,
            IObsWebsocketSettingUseCase settingUseCase)
        {
            this.useCase = useCase;
            this.settingUseCase = settingUseCase;
        }


        public bool Connect()
        {
            var url = settingUseCase.ViewModel.ServerAddress.Value;
            var pass = settingUseCase.ViewModel.Password.Value;
            return useCase.Connect(url, pass);
        }

        public void Disconnect()
        {
            useCase.Disconnect();
        }
    }

    public class ObsWebsocketCommunicationControllerInstaller : Installer<ObsWebsocketCommunicationControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketCommunicationController>().AsSingle();
        }
    }
}