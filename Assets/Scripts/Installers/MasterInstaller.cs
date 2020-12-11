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

            Container.BindInterfacesAndSelfTo<TimecodeDecoderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimecodeDecoderRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<OscSenderDataStore>().AsSingle();
            Container.BindInterfacesAndSelfTo<SenderRepository>().AsSingle();

            Container.BindInterfacesAndSelfTo<TimecodeDisplayUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SendToEndpointUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TimecodeEvaluationUseCase>().AsSingle().NonLazy();

        }
    }
}