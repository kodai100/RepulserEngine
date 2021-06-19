using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installers
{
    public class ConnectionCheckInstaller : Installer<ConnectionCheckInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ConnectionCheckRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConnectionCheckUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConnectionCheckController>().AsSingle();
        }
    }
}