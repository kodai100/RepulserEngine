using System;
using UniRx;

namespace ProjectBlue.RepulserEngine
{
    public class RepulserUseCase : IDisposable
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public RepulserUseCase(IEndPointListPresenter endpointListPresenter, ISignalPulserPresenter signalPulserPresenter)
        {

            signalPulserPresenter.OnSendAsObservable.Subscribe(message =>
            {
                
                endpointListPresenter.Send(message.OscAddress, message.OscData);
                
            }).AddTo(_disposable);

        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }

}

