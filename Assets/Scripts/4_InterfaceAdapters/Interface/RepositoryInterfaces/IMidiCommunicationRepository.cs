using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface IMidiCommunicationRepository
    {
        IObservable<MidiData> OnMidiAsObservable { get; }
        public void Connect();
        public void Disconnect();
    }
}