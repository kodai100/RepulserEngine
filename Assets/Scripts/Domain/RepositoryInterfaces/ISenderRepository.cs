using System.Net;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface ISenderRepository
    {
        void Send(IPEndPoint endpoint, string oscAddress, string oscData);
        void Send(IPEndPoint endpoint, string oscAddress, float oscData);
        void Send(IPEndPoint endpoint, string oscAddress, int oscData);
    }

}

