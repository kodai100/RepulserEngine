using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installers
{
    public class ObsWebsocketSettingInstaller : Installer<ObsWebsocketSettingInstaller>
    {
        public override void InstallBindings()
        {
            ObsWebsocketSettingRepositoryInstaller.Install(Container);
            ObsWebsocketSettingUseCaseInstaller.Install(Container);
            ObsWebsocketSettingControllerInstaller.Install(Container);

            ObsWebsocketCommunicationRepositoryInstaller.Install(Container);
            ObsWebsocketCommunicationUseCaseInstaller.Install(Container);
            ObsWebsocketCommunicationControllerInstaller.Install(Container);
        }
    }
}