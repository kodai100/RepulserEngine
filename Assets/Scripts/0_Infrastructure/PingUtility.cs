using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Ping = System.Net.NetworkInformation.Ping;

public class PingUtility
{

    public static async Task<bool> Ping(IPAddress address, int timeout = 5000)
    {

        var pingSender = new Ping();
        
        var buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        var options = new PingOptions(64, true);
        
        var reply = await pingSender.SendPingAsync(address, timeout, buffer, options);

        Debug.Log(
            $"Reply from {reply.Address}: bytes={reply.Buffer.Length} time={reply.RoundtripTime}ms TTL={reply.Options.Ttl}");
        
        return reply.Status == IPStatus.Success;

    }

}