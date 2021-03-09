using System;
using System.Net;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.ViewModel
{
    
    [Serializable]
    public class EndpointSettingViewModel
    {

        public int Index { get; set; } = -1;

        public ReactiveProperty<string> ip = new ReactiveProperty<string>();
        public ReactiveProperty<int> port = new ReactiveProperty<int>();
        public ReactiveProperty<string> endpointName = new ReactiveProperty<string>();
        public ReactiveProperty<int> offsetFrame = new ReactiveProperty<int>();

        public ReactiveProperty<bool> connected = new ReactiveProperty<bool>();

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