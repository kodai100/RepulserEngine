using System;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public interface IObsWebsocketCommunicationUseCase
    {
        IObservable<string> OnConnected { get; }
        IObservable<string> OnDisconnected { get; }
        IObservable<string> OnSceneChanged { get; }

        bool Connect(string serverUrl, string password);
        void Disconnect();

        public void SetScene(string sceneName);

        public void RestartMediaSource(string mediaSourceName);
    }
}