using System;
using System.Net;
using UniRx;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class SendToEndpointUseCase : IDisposable
    {

        private CompositeDisposable _disposable = new CompositeDisposable();

        private IEndPointListPresenter endpointListPresenter;
        private IOverlayPresenter overlayPresenter;

        private ISenderRepository senderRepository;
        
        public SendToEndpointUseCase(
            IEndPointListPresenter endpointListPresenter,
            IOverlayPresenter overlayPresenter,
            ISenderRepository senderRepository)
        {
            this.endpointListPresenter = endpointListPresenter;
            this.overlayPresenter = overlayPresenter;
            this.senderRepository = senderRepository;
        }
        
        public void Send(string oscAddress, string oscData)
        {

            foreach (var setting in endpointListPresenter.EndpointSettingList)
            {
                SendIntermediator(setting.EndPoint, oscAddress, oscData);
            }
            
            Logger.Instance.Log($"{oscAddress} : {oscData}");
            
            overlayPresenter.Trigger();
        }

        public void Send(string oscAddress, string oscData, string ipAddress)
        {

            foreach (var setting in endpointListPresenter.EndpointSettingList)
            {
                // TODO: Consider target port
                if (setting.EndPoint.Address.ToString().Equals(ipAddress.ToString()))
                    SendIntermediator(setting.EndPoint, oscAddress, oscData);
            }
            
            Logger.Instance.Log($"{oscAddress} : {oscData}");
            
            overlayPresenter.Trigger();
        }

        private void SendIntermediator(IPEndPoint endPoint, string oscAddress, string oscData)
        {

            // float detection
            if (oscData.Contains("."))
            {
                if (float.TryParse(oscData, out var floaResult))
                {
                    senderRepository.Send(endPoint, oscAddress, floaResult);
                }
                else
                {
                    senderRepository.Send(endPoint, oscAddress, oscData);
                }

                return;
            }
            
            // int detection
            if (int.TryParse(oscData, out var intResult))
            {
                senderRepository.Send(endPoint, oscAddress, intResult);
            }
            else
            {
                senderRepository.Send(endPoint, oscAddress, oscData);
            }
            
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }

}

