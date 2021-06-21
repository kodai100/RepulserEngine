using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IMidiCommunicationController
    {
        IObservable<MidiData> OnMidiAsObservable { get; }
        void Connect();
        void Disconnect();
    }
}