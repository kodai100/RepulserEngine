

using ProjectBlue.RepulserEngine.Domain.UseCase;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{

    public class OffsetFrameController : IOffsetFrameController 
    {

        IOffsetFrameUseCase useCase;

        public OffsetFrameController(IOffsetFrameUseCase useCase)
        {
            this.useCase = useCase;
        }

    }

    public class OffsetFrameControllerInstaller : Installer<OffsetFrameControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OffsetFrameController>().AsSingle();
        }
    }
}
