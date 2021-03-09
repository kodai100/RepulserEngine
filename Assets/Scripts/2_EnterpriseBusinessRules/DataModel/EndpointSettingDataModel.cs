using System;
using System.Net;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.DataModel
{
    
    [Serializable]
    public class EndpointSettingDataModel
    {

        [SerializeField] private string ip;
        [SerializeField] private int port;
        [SerializeField] private string endpointName;
        [SerializeField] private int offsetFrame;

        public IPEndPoint EndPoint => new IPEndPoint(IPAddress.Parse(ip), port);
        public string EndPointName => endpointName;
        public int OffsetFrame => offsetFrame;

        public EndpointSettingDataModel(IPEndPoint endPoint, string endPointName, int offsetFrame)
        {
            this.endpointName = endPointName;
            this.offsetFrame = offsetFrame;

            ip = endPoint.Address.ToString();
            port = endPoint.Port;
        }

        public EndpointSettingDataModel() : this(new IPEndPoint(IPAddress.None, 2974), "NULL", 0)
        {
            
        }
    }

}