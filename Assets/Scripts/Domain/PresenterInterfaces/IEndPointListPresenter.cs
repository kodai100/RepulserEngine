using System;

namespace ProjectBlue.RepulserEngine
{
    public interface IEndPointListPresenter
    {
        void Send(string oscAddress, string oscData);
    }

    public interface ISignalPulserPresenter
    {
        IObservable<Message> OnSendAsObservable { get; }
    }
}