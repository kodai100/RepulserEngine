using System;
using System.Net;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.ViewModel
{
    
    [Serializable]
    public class EndpointSettingViewModel
    {

        public string hash = Guid.NewGuid().ToString();

        public ReactiveProperty<string> ip;
        public ReactiveProperty<int> port;
        public ReactiveProperty<string> endpointName;
        public ReactiveProperty<int> offsetFrame;

        public ReactiveProperty<bool> connected;

        public IPEndPoint EndPoint => new IPEndPoint(IPAddress.Parse(ip.Value), port.Value);
        public string EndPointName => endpointName.Value;
        public int OffsetFrame => offsetFrame.Value;

        public EndpointSettingViewModel(IPEndPoint endPoint, string endPointName, int offsetFrame)
        {
            this.endpointName.Value = endPointName;
            this.offsetFrame.Value = offsetFrame;

            ip.Value = endPoint.Address.ToString();
            port.Value = endPoint.Port;

            connected.Value = false;
        }

        public EndpointSettingViewModel() : this(new IPEndPoint(IPAddress.None, 2974), "NULL", 0)
        {
            
        }
    }

}