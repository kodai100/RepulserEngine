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
            // Timecode decode and display process
            Container.BindInterfacesAndSelfTo<TimecodeDecoderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDisplayPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDisplayUseCase>().AsSingle().NonLazy();

            // Endpoint Setting Save and Load
            Container.BindInterfacesAndSelfTo<EndpointSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointListPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingUseCase>().AsSingle().NonLazy();
            
            // Timecode Setting Save and Load
            Container.BindInterfacesAndSelfTo<TimecodeSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeSettingRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeListPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeSettingUseCase>().AsSingle().NonLazy();

            // Command Setting Save and Load
            Container.BindInterfacesAndSelfTo<CommandSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingListPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingUseCase>().AsSingle().NonLazy();
            
            
            // Container.BindInterfacesAndSelfTo<Main>().AsSingle().NonLazy();

        }
    }
}