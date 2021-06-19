

using ProjectBlue.RepulserEngine.Repository;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class MidiCommunicationUseCase : IMidiCommunicationUseCase
    {

        IMidiCommunicationRepository repository;

        public MidiCommunicationUseCase(IMidiCommunicationRepository repository)
        {
            this.repository = repository;
        }
    }

    public class MidiCommunicationUseCaseInstaller : Installer<MidiCommunicationUseCaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MidiCommunicationUseCase>().AsSingle();
        }
    }
}
