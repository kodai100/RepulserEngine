using System.Net;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class OscSenderRepository : ISenderRepository
    {
        private OscSender oscSender = new OscSender();

        public OscSenderRepository()
        {
        }

        public void Send(IPEndPoint endpoint, string oscAddress, string oscData)
        {
            oscSender.Send(endpoint, oscAddress, oscData);
            Debug.Log($"[Repository] Send : {endpoint}, {oscAddress}, {oscData}");
        }

        public void Send(IPEndPoint endpoint, string oscAddress, float oscData)
        {
            oscSender.Send(endpoint, oscAddress, oscData);
            Debug.Log($"[Repository] Send : {endpoint}, {oscAddress}, {oscData}");
        }

        public void Send(IPEndPoint endpoint, string oscAddress, int oscData)
        {
            oscSender.Send(endpoint, oscAddress, oscData);
            Debug.Log($"[Repository] Send : {endpoint}, {oscAddress}, {oscData}");
        }
    }
}