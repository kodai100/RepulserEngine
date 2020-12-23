using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Entity;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installer
{
    public class MasterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // presenters is registered via zenject binding component

            Container.BindInterfacesAndSelfTo<PulseDisplayPresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<KeyboardInputDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<KeyboardInputRepository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TimecodeDecoderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<OscSenderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<SenderRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<PulseSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<PulseSettingRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<CommandSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingRepository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EndpointSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<EndpointSettingUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TimecodeDisplayUseCase>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<TimecodeEvaluationUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<SendToEndpointUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<PulseSettingUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingUseCase>().AsSingle();
            

            Container.BindInterfacesAndSelfTo<Main>().AsSingle().NonLazy();

        }
    }
}