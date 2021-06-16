using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installers
{
    public class OnAirSettingInstaller : Installer<OnAirSettingInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OnAirSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnAirSettingRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnAirSettingUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<OnAirSettingController>().AsSingle();
        }
    }
}