using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class SenderAssembly : MonoBehaviour
{
    private static SenderAssembly instance;
    public static SenderAssembly Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = (SenderAssembly)FindObjectOfType(typeof(SenderAssembly));

            if (instance == null)
            {
                Debug.LogWarning(typeof(SenderAssembly) + "is nothing");
            }

            return instance;
        }
    }

    [SerializeField] private InputField defaultRemoteIpField;
    [SerializeField] private int destinationPort = 10010;

    [SerializeField] private Text statusText;
    
    private OscSender _oscSender;
    private IPEndPoint defaultDestination;
    
    private void Start()
    {
        _oscSender = new OscSender();
        defaultDestination = new IPEndPoint(IPAddress.Parse("255.255.255.255"), destinationPort);
    }

    private void SetDefaultDestination()
    {
        defaultDestination = new IPEndPoint(IPAddress.Parse(defaultRemoteIpField.text), destinationPort);
    }

    public void Send(string oscAddress, string oscData)
    {
        _oscSender.Send(defaultDestination, oscAddress, oscData);
        statusText.text = $"{oscAddress} : {oscData}";
    }

    public void Send(string overrideIp, string oscAddress, string oscData)
    {
        var endpoint = new IPEndPoint(IPAddress.Parse(overrideIp), destinationPort);
        _oscSender.Send(endpoint, oscAddress, oscData);

        statusText.text = $"{oscAddress} : {oscData}";
    }
}
