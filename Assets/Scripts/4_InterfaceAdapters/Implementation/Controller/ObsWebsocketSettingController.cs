using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class ObsWebsocketSettingController : IObsWebsocketSettingController
    {
        public ObsWebsocketSettingViewModel ViewModel => useCase.ViewModel;

        private IObsWebsocketSettingUseCase useCase;

        public ObsWebsocketSettingController(IObsWebsocketSettingUseCase useCase)
        {
            this.useCase = useCase;
        }

        public void Save()
        {
            useCase.Save();
        }

        public void Load()
        {
            useCase.Load();
        }
    }

    public class ObsWebsocketSettingControllerInstaller : Installer<ObsWebsocketSettingControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketSettingController>().AsSingle();
        }
    }
}