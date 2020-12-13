using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installer
{
    public class MasterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // presenters is registered via zenject binding component

            Container.BindInterfacesAndSelfTo<TimecodeDecoderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<OscSenderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<SenderRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<PulseSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<PulseSettingRepository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EndpointSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<TimecodeDisplayUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SendToEndpointUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TimecodeEvaluationUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PulseSettingUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EndpointSettingUseCase>().AsSingle().NonLazy();

        }
    }
}