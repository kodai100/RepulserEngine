using System.Net;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine
{
    public class SenderAssembly : SingletonMonoBehaviour<SenderAssembly>
    {
    
        [SerializeField] private InputField defaultRemoteIpField;
        [SerializeField] private Button defaultRemoteIpSetButton;
        [SerializeField] private int destinationPort = 10100;
        
        private OscSender _oscSender;
        private IPEndPoint _defaultDestination;
        
        private void Start()
        {
            _oscSender = new OscSender();
            
            _defaultDestination = Load();
    
            defaultRemoteIpSetButton.OnClickAsObservable().Subscribe(_ =>
            {
                _defaultDestination = new IPEndPoint(IPAddress.Parse(defaultRemoteIpField.text), destinationPort);
                Save();
            }).AddTo(this);
        }
    
        private IPEndPoint Load()
        {
            var loadedIp = PlayerPrefs.GetString("IP");
    
            if (!string.IsNullOrEmpty(loadedIp))
            {
                defaultRemoteIpField.text = loadedIp;
                return new IPEndPoint(IPAddress.Parse(loadedIp), destinationPort);
            }
            else
            {
                defaultRemoteIpField.text = "255.255.255.255";
                return new IPEndPoint(IPAddress.Broadcast, destinationPort);
            }
        }
    
        private void Save()
        {
            PlayerPrefs.SetString("IP", defaultRemoteIpField.text);
        }
    
        public void Send(string oscAddress, string oscData)
        {
            _oscSender.Send(_defaultDestination, oscAddress, oscData);
            Logger.Instance.Log($"{oscAddress} : {oscData}");
            
            Overlay.Instance.Trigger();
        }
    
        public void Send(string overrideIp, string oscAddress, string oscData)
        {
            var endpoint = new IPEndPoint(IPAddress.Parse(overrideIp), destinationPort);
            _oscSender.Send(endpoint, oscAddress, oscData);
    
            Logger.Instance.Log($"{oscAddress} : {oscData}");
            
            Overlay.Instance.Trigger();
        }
    
        private void OnDestroy()
        {
            _oscSender.Dispose();
        }
    }
}