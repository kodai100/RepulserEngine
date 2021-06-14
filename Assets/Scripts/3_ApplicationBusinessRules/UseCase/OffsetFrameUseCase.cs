

using ProjectBlue.RepulserEngine.Repository;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class OffsetFrameUseCase : IOffsetFrameUseCase
    {

        IOffsetFrameRepository repository;

        public OffsetFrameUseCase(IOffsetFrameRepository repository)
        {
            this.repository = repository;
        }
    }

    public class OffsetFrameUseCaseInstaller : Installer<OffsetFrameUseCaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OffsetFrameUseCase>().AsSingle();
        }
    }
}
