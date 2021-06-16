using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Data.DataStore;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.UseCase;
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

            ObsWebsocketSettingInstaller.Install(Container);

            GlobalFrameOffsetSettingDataStoreInstaller.Install(Container);
            GlobalFrameOffsetSettingRepositoryInstaller.Install(Container);
            GlobalFrameOffsetSettingUseCaseInstaller.Install(Container);
            GlobalFrameOffsetSettingControllerInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<TimecodeDecoderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderUseCase>().AsSingle();

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
            Container.BindInterfacesAndSelfTo<OverlayUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<OverlayController>().AsSingle();

            // Trigger Button
            Container.BindInterfacesAndSelfTo<CommandTriggerUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandTriggerController>().AsSingle();

            Container.BindInterfacesAndSelfTo<TimecodeDisplayController>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandSettingListController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndpointSettingListController>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeSettingListController>().AsSingle();

            // Controllers
            Container.BindInterfacesAndSelfTo<SignalTriggerController>().AsSingle().NonLazy();
        }
    }
}