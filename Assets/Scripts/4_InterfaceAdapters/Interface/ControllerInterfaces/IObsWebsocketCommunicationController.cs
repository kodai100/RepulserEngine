using System;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IObsWebsocketCommunicationController
    {
        IObservable<string> OnConnected { get; }
        IObservable<string> OnDisconnected { get; }
        IObservable<string> OnSceneChanged { get; }

        bool Connect();
        void Disconnect();
    }
}