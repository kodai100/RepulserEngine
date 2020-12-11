using System.Net;

namespace ProjectBlue.RepulserEngine.DataStore
{
    
    public class OscSenderDataStore : IOscSenderDataStore
    {
        
        private OscSender oscSender = new OscSender();

        public OscSenderDataStore()
        {
            
        }

        public void Send(IPEndPoint endpoint, string oscAddress, string oscData)
        {
            oscSender.Send(endpoint, oscAddress, oscData);
        }

        public void Send(IPEndPoint endpoint, string oscAddress, float oscData)
        {
            oscSender.Send(endpoint, oscAddress, oscData);
        }

        public void Send(IPEndPoint endpoint, string oscAddress, int oscData)
        {
            oscSender.Send(endpoint, oscAddress, oscData);
        }
    }

}

