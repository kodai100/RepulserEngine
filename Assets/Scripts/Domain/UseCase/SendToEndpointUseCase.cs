using System;
using UniRx;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class SendToEndpointUseCase : IDisposable
    {
        
        // TODO move to repository
        private OscSender sender = new OscSender();
        
        private CompositeDisposable _disposable = new CompositeDisposable();

        private IEndPointListPresenter endpointListPresenter;
        private IOverlayPresenter overlayPresenter;

        private ITimecodeDecoderRepository timecodeDecoderRepository;
        
        public SendToEndpointUseCase(
            IEndPointListPresenter endpointListPresenter,
            IOverlayPresenter overlayPresenter,
            ITimecodeDecoderRepository timecodeDecoderRepository)
        {
            this.endpointListPresenter = endpointListPresenter;
            this.overlayPresenter = overlayPresenter;

            this.timecodeDecoderRepository = timecodeDecoderRepository;

        }
        
        public void Send(string oscAddress, string oscData)
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

