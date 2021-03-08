using System.Net;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public interface IOscSenderDataStore
    {
        void Send(IPEndPoint endpoint, string oscAddress, string oscData);
        void Send(IPEndPoint endpoint, string oscAddress, float oscData);
        void Send(IPEndPoint endpoint, string oscAddress, int oscData);
    }
}