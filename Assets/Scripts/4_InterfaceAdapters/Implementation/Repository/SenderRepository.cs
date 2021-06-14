using System.Net;
using ProjectBlue.RepulserEngine.DataStore;

namespace ProjectBlue.RepulserEngine.Repository
{

    public class SenderRepository : ISenderRepository
    {

        private IOscSenderDataStore oscSenderDataStore;
        
        public SenderRepository(IOscSenderDataStore oscSenderDataStore)
        {
            this.oscSenderDataStore = oscSenderDataStore;
        }

        public void Send(IPEndPoint endpoint, string oscAddress, string oscData)
        {
            oscSenderDataStore.Send(endpoint, oscAddress, oscData);
        }

        public void Send(IPEndPoint endpoint, string oscAddress, float oscData)
        {
            oscSenderDataStore.Send(endpoint, oscAddress, oscData);
        }

        public void Send(IPEndPoint endpoint, string oscAddress, int oscData)
        {
            oscSenderDataStore.Send(endpoint, oscAddress, oscData);
        }
    }
    
}