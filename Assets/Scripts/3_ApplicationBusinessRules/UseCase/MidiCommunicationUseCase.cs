using System;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Repository;
using Zenject;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class MidiCommunicationUseCase : IMidiCommunicationUseCase
    {
        public IObservable<MidiData> OnMidiAsObservable => repository.OnMidiAsObservable;

        IMidiCommunicationRepository repository;

        public MidiCommunicationUseCase(IMidiCommunicationRepository repository)
        {
            this.repository = repository;
        }

        public void Connect()
        {
            repository.Connect();
        }

        public void Disconnect()
        {
            repository.Disconnect();
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