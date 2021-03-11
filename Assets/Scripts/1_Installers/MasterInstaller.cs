using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installers
{
    public class MasterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.BindInterfacesAndSelfTo<KeyboardInputDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<KeyboardInputRepository>().AsSingle();

            ConnectionCheckInstaller.Install(Container);
            
            OnAirSettingInstaller.Install(Container);
            
            Container.BindInterfacesAndSelfTo<TimecodeDecoderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderRepository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<OscSenderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<SenderRepository>().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<CommandSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingRepository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EndpointSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingRepository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TimecodeSettingDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeSettingRepository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TimecodeDisplayUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<SendToEndpointUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeSettingUseCase>().AsSingle();
            
            // こいつは独立して動く
            Container.BindInterfacesAndSelfTo<OverlayPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeEvaluationUseCase>().AsSingle().NonLazy();

            // Trigger Button
            Container.BindInterfacesAndSelfTo<CommandTriggerPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandTriggerUseCase>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TimecodeDisplayPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingListPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingListPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeSettingListPresenter>().AsSingle();

            // Controllers
            Container.BindInterfacesAndSelfTo<Trigger>().AsSingle().NonLazy();
        }
    }
}