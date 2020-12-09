using System.Net;
using System.Net.Sockets;
using Osc;

public class OscSender
{
    private static UdpClient _client;

    public OscSender()
    {
        _client = new UdpClient(0, AddressFamily.InterNetwork);
    }

    public void Send(IPEndPoint endPoint, string oscPath, string data)
    {
        var oscEnc = new OscMessageEncoder(oscPath);
        oscEnc.Add(data);

        var byteData = oscEnc.Encode();
        _client.Send(oscEnc.Encode(), byteData.Length, endPoint);
    }

}
