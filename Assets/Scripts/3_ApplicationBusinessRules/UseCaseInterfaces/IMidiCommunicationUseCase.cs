using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public interface IMidiCommunicationUseCase
    {
        IObservable<MidiData> OnMidiAsObservable { get; }
    }
}