using System;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class MidiCommunicationController : IMidiCommunicationController
    {
        public IObservable<MidiData> OnMidiAsObservable => useCase.OnMidiAsObservable;

        IMidiCommunicationUseCase useCase;

        public MidiCommunicationController(IMidiCommunicationUseCase useCase)
        {
            this.useCase = useCase;
        }

        public void Connect()
        {
            useCase.Connect();
        }

        public void Disconnect()
        {
            useCase.Disconnect();
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