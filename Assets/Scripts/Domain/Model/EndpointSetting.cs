using System.Net;

namespace ProjectBlue.RepulserEngine.Domain.Model
{
    public class EndpointSetting
    {
        public IPEndPoint EndPoint { get; }
        public string EndPointName { get; }
        public int OffsetFrame { get; }
    
        public EndpointSetting(IPEndPoint endPoint, string endPointName, int offsetFrame)
        {
            EndPoint = endPoint;
            EndPointName = endPointName;
            OffsetFrame = offsetFrame;
        }
    }

}