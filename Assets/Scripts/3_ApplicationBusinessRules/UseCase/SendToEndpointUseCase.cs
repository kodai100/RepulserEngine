using System;
using System.Net;
using ProjectBlue.RepulserEngine.DataStore;
using UniRx;
using ProjectBlue.RepulserEngine.Repository;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class SendToEndpointUseCase : IDisposable
    {

        private CompositeDisposable _disposable = new CompositeDisposable();

        private ISenderRepository senderRepository;
        private IEndpointSettingRepository endpointSettingRepository;
        
        public SendToEndpointUseCase(ISenderRepository senderRepository, IEndpointSettingRepository endpointSettingRepository)
        {
            this.senderRepository = senderRepository;
            this.endpointSettingRepository = endpointSettingRepository;
        }
        
        public void Send(string oscAddress, string oscData)
        {

            foreach (var setting in endpointSettingRepository.EndPointList)
            {
                SendIntermediator(setting.EndPoint, oscAddress, oscData);
            }
        }

        public void SendToSpecificIP(string ipAddress, string oscAddress, string oscData)
        {
            var count = 0;
            var arr =  ipAddress.Split(',');
            
            foreach (var setting in endpointSettingRepository.EndPointList)
            {
                // TODO: Consider target port
                if (setting.EndPoint.Address.ToString().Equals(ipAddress))
                {
                    SendIntermediator(setting.EndPoint, oscAddress, oscData);
                }
                    
                foreach (var k in arr)
                {
                    if (!k.Equals(count.ToString())) continue;
                    
                    Debug.Log($"Specific ip: {setting.EndPoint.Address}");
                    SendIntermediator(setting.EndPoint, oscAddress, oscData);
                }
                
                count++;
            }
            
            Debug.Log($"{oscAddress} : {oscData}");
        }


        private void SendIntermediator(IPEndPoint endPoint, string oscAddress, string oscData)
        {

            if (string.IsNullOrEmpty(oscData))
            {
                senderRepository.Send(endPoint, oscAddress, "null");
                return;
            }
            
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

