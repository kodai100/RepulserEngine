using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class ConnectionCheckRepository : IConnectionCheckRepository
    {
        private int checkNum = 4;

        public ConnectionCheckRepository()
        {
        }

        public async Task<bool> Check(IPAddress address)
        {
            Debug.Log($"Check : {address}");

            var checkSum = 0;

            for (var i = 0; i < checkNum; i++)
            {
                var result = await PingUtility.Ping(address);

                if (result)
                {
                    checkSum++;
                }
                else
                {
                    Debug.Log($"Failed : {address}");
                    return false;
                }
            }

            Debug.Log($"Success : {address}");
            return true;
        }
    }
}