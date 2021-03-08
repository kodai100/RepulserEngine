using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;
using Ping = System.Net.NetworkInformation.Ping;

namespace ProjectBlue.RepulserEngine.DataStore
{
    
    public class ConnectionCheckDataStore : IConnectionCheckDataStore
    {
        
        private int checkNum = 4;

        public ConnectionCheckDataStore()
        {
            
        }
        
        public async Task<bool> Check(IPAddress address)
        {
            
            var sender = new Ping();
            var checkSum = 0;
            
            for(var i = 0; i < checkNum; i++)
            {
                var reply = sender.Send(address);
                if(reply.Status == IPStatus.Success)
                {
                    Debug.Log(
                        $"Reply from {reply.Address}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");
                    checkSum++;
                }
                else
                {
                    return false;
                }
                
                if(i < checkNum-1)
                {
                    await Task.Delay(300);
                }
            }

            return checkSum == checkNum;
        }
    }

}