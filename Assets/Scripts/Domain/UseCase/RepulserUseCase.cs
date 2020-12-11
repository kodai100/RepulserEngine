using System;
using UniRx;
using ProjectBlue.RepulserEngine.Presentation;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class RepulserUseCase : IDisposable
    {
        
        // TODO move to repository
        private OscSender sender = new OscSender();
        
        private CompositeDisposable _disposable = new CompositeDisposable();

        private IEndPointListPresenter endpointListPresenter;
        private IOverlayPresenter overlayPresenter;
        
        public RepulserUseCase(
            IEndPointListPresenter endpointListPresenter, 
            ISignalPulserPresenter signalPulserPresenter, IOverlayPresenter overlayPresenter)
        {
            this.endpointListPresenter = endpointListPresenter;
            this.overlayPresenter = overlayPresenter;

            signalPulserPresenter.OnSendAsObservable.Subscribe(message =>
            {
                
                Send(message.OscAddress, message.OscData);
                
            }).AddTo(_disposable);
            
        }
        
        private void Send(string oscAddress, string oscData)
        {

            foreach (var setting in endpointListPresenter.EndpointSettingList)
            {
                sender.Send(setting.EndPoint, oscAddress, oscData);
            }
            
            Logger.Instance.Log($"{oscAddress} : {oscData}");
            
            overlayPresenter.Trigger();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }

}

