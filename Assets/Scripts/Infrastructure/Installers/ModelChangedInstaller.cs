using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installer
{
    public class ModelChangedInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // presenters is registered via zenject binding component

            Container.BindInterfacesAndSelfTo<TimecodeDecoderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDisplayPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDisplayUseCase>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EndpointSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointListPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingUseCase>().AsSingle().NonLazy();
            
            


            // Container.BindInterfacesAndSelfTo<Main>().AsSingle().NonLazy();

        }
    }
}