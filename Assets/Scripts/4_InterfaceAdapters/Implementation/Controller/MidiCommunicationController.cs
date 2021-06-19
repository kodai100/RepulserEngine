

using ProjectBlue.RepulserEngine.Domain.UseCase;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{

    public class MidiCommunicationController : IMidiCommunicationController 
    {

        IMidiCommunicationUseCase useCase;

        public MidiCommunicationController(IMidiCommunicationUseCase useCase)
        {
            this.useCase = useCase;
        }

    }

    public class MidiCommunicationControllerInstaller : Installer<MidiCommunicationControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MidiCommunicationController>().AsSingle();
        }
    }
}
