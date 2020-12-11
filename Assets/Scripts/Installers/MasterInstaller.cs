using ProjectBlue.RepulserEngine.Domain.UseCase;
using Zenject;

namespace ProjectBlue.RepulserEngine.Installer
{
    public class MasterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.BindInterfacesAndSelfTo<RepulserUseCase>().AsSingle().NonLazy();

        }
    }
}